var Slick = {
    Event: Event,
    EventData: EventData,
    EventHandler: EventHandler,
    Range: Range,
    NonDataRow: NonDataItem,
    Group: Group,
    GroupTotals: GroupTotals,
    EditorLock: EditorLock,
    GlobalEditorLock: new EditorLock(),
    TreeColumns: TreeColumns,
    keyCode: {
        BACKSPACE: 8,
        DELETE: 46,
        DOWN: 40,
        END: 35,
        ENTER: 13,
        ESCAPE: 27,
        HOME: 36,
        INSERT: 45,
        LEFT: 37,
        PAGE_DOWN: 34,
        PAGE_UP: 33,
        RIGHT: 39,
        TAB: 9,
        UP: 38,
        SPACE: 32
    }
};
export default Slick;
function EventData() {
    var isPropagationStopped = false;
    var isImmediatePropagationStopped = false;
    this.stopPropagation = function () {
        isPropagationStopped = true;
    };
    this.isPropagationStopped = function () {
        return isPropagationStopped;
    };
    this.stopImmediatePropagation = function () {
        isImmediatePropagationStopped = true;
    };
    this.isImmediatePropagationStopped = function () {
        return isImmediatePropagationStopped;
    };
}
function Event() {
    var handlers = [];
    this.subscribe = function (fn) {
        handlers.push(fn);
    };
    this.unsubscribe = function (fn) {
        for (var i = handlers.length - 1; i >= 0; i--) {
            if (handlers[i] === fn) {
                handlers.splice(i, 1);
            }
        }
    };
    this.notify = function (args, e, scope) {
        e = e || new EventData();
        scope = scope || this;
        var returnValue;
        for (var i = 0; i < handlers.length && !(e.isPropagationStopped() || e.isImmediatePropagationStopped()); i++) {
            returnValue = handlers[i].call(scope, e, args);
        }
        return returnValue;
    };
}
function EventHandler() {
    var handlers = [];
    this.subscribe = function (event, handler) {
        handlers.push({
            event: event,
            handler: handler
        });
        event.subscribe(handler);
        return this;
    };
    this.unsubscribe = function (event, handler) {
        var i = handlers.length;
        while (i--) {
            if (handlers[i].event === event && handlers[i].handler === handler) {
                handlers.splice(i, 1);
                event.unsubscribe(handler);
                return;
            }
        }
        return this;
    };
    this.unsubscribeAll = function () {
        var i = handlers.length;
        while (i--) {
            handlers[i].event.unsubscribe(handlers[i].handler);
        }
        handlers = [];
        return this;
    };
}
function Range(fromRow, fromCell, toRow, toCell) {
    if (toRow === undefined && toCell === undefined) {
        toRow = fromRow;
        toCell = fromCell;
    }
    this.fromRow = Math.min(fromRow, toRow);
    this.fromCell = Math.min(fromCell, toCell);
    this.toRow = Math.max(fromRow, toRow);
    this.toCell = Math.max(fromCell, toCell);
    this.isSingleRow = function () {
        return this.fromRow == this.toRow;
    };
    this.isSingleCell = function () {
        return this.fromRow == this.toRow && this.fromCell == this.toCell;
    };
    this.contains = function (row, cell) {
        return row >= this.fromRow && row <= this.toRow && cell >= this.fromCell && cell <= this.toCell;
    };
    this.toString = function () {
        if (this.isSingleCell()) {
            return "(" + this.fromRow + ":" + this.fromCell + ")";
        }
        else {
            return "(" + this.fromRow + ":" + this.fromCell + " - " + this.toRow + ":" + this.toCell + ")";
        }
    };
}
function NonDataItem() {
    this.__nonDataRow = true;
}
function Group() {
    this.__group = true;
    this.level = 0;
    this.count = 0;
    this.value = null;
    this.title = null;
    this.collapsed = false;
    this.totals = null;
    this.rows = [];
    this.groups = null;
    this.groupingKey = null;
}
Group.prototype = new NonDataItem();
Group.prototype.equals = function (group) {
    return this.value === group.value && this.count === group.count && this.collapsed === group.collapsed && this.title === group.title;
};
function GroupTotals() {
    this.__groupTotals = true;
    this.group = null;
    this.initialized = false;
}
GroupTotals.prototype = new NonDataItem();
function EditorLock() {
    var activeEditController = null;
    this.isActive = function (editController) {
        return (editController ? activeEditController === editController : activeEditController !== null);
    };
    this.activate = function (editController) {
        if (editController === activeEditController) {
            return;
        }
        if (activeEditController !== null) {
            throw "SlickGrid.EditorLock.activate: an editController is still active, can't activate another editController";
        }
        if (!editController.commitCurrentEdit) {
            throw "SlickGrid.EditorLock.activate: editController must implement .commitCurrentEdit()";
        }
        if (!editController.cancelCurrentEdit) {
            throw "SlickGrid.EditorLock.activate: editController must implement .cancelCurrentEdit()";
        }
        activeEditController = editController;
    };
    this.deactivate = function (editController) {
        if (activeEditController !== editController) {
            throw "SlickGrid.EditorLock.deactivate: specified editController is not the currently active one";
        }
        activeEditController = null;
    };
    this.commitCurrentEdit = function () {
        return (activeEditController ? activeEditController.commitCurrentEdit() : true);
    };
    this.cancelCurrentEdit = function cancelCurrentEdit() {
        return (activeEditController ? activeEditController.cancelCurrentEdit() : true);
    };
}
function TreeColumns(treeColumns) {
    var columnsById = {};
    function init() {
        mapToId(treeColumns);
    }
    function mapToId(columns) {
        columns.forEach(function (column) {
            columnsById[column.id] = column;
            if (column.columns)
                mapToId(column.columns);
        });
    }
    function filter(node, condition) {
        return node.filter(function (column) {
            var valid = condition.call(column);
            if (valid && column.columns)
                column.columns = filter(column.columns, condition);
            return valid && (!column.columns || column.columns.length);
        });
    }
    function sort(columns, grid) {
        columns.sort(function (a, b) {
            var indexA = getOrDefault(grid.getColumnIndex(a.id)), indexB = getOrDefault(grid.getColumnIndex(b.id));
            return indexA - indexB;
        })
            .forEach(function (column) {
            if (column.columns)
                sort(column.columns, grid);
        });
    }
    function getOrDefault(value) {
        return typeof value === 'undefined' ? -1 : value;
    }
    function getDepth(node) {
        if (node.length)
            for (var i in node)
                return getDepth(node[i]);
        else if (node.columns)
            return 1 + getDepth(node.columns);
        else
            return 1;
    }
    function getColumnsInDepth(node, depth, current) {
        var columns = [];
        current = current || 0;
        if (depth == current) {
            if (node.length)
                node.forEach(function (n) {
                    if (n.columns)
                        n.extractColumns = function () {
                            return extractColumns(n);
                        };
                });
            return node;
        }
        else
            for (var i in node)
                if (node[i].columns) {
                    columns = columns.concat(getColumnsInDepth(node[i].columns, depth, current + 1));
                }
        return columns;
    }
    function extractColumns(node) {
        var result = [];
        if (node.hasOwnProperty('length')) {
            for (var i = 0; i < node.length; i++)
                result = result.concat(extractColumns(node[i]));
        }
        else {
            if (node.hasOwnProperty('columns'))
                result = result.concat(extractColumns(node.columns));
            else
                return node;
        }
        return result;
    }
    function cloneTreeColumns() {
        return $.extend(true, [], treeColumns);
    }
    init();
    this.hasDepth = function () {
        for (var i in treeColumns)
            if (treeColumns[i].hasOwnProperty('columns'))
                return true;
        return false;
    };
    this.getTreeColumns = function () {
        return treeColumns;
    };
    this.extractColumns = function () {
        return this.hasDepth() ? extractColumns(treeColumns) : treeColumns;
    };
    this.getDepth = function () {
        return getDepth(treeColumns);
    };
    this.getColumnsInDepth = function (depth) {
        return getColumnsInDepth(treeColumns, depth, 0);
    };
    this.getColumnsInGroup = function (groups) {
        return extractColumns(groups);
    };
    this.visibleColumns = function () {
        return filter(cloneTreeColumns(), function () {
            return this.visible;
        });
    };
    this.filter = function (condition) {
        return filter(cloneTreeColumns(), condition);
    };
    this.reOrder = function (grid) {
        return sort(treeColumns, grid);
    };
    this.getById = function (id) {
        return columnsById[id];
    };
    this.getInIds = function (ids) {
        return ids.map(function (id) {
            return columnsById[id];
        });
    };
}
