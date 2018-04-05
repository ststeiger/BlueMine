import Slick from './slick.core.js';
export default GroupItemMetadataProvider;
function GroupItemMetadataProvider(options) {
    var _grid;
    var _defaults = {
        groupCssClass: "slick-group",
        groupTitleCssClass: "slick-group-title",
        totalsCssClass: "slick-group-totals",
        groupFocusable: true,
        totalsFocusable: false,
        toggleCssClass: "slick-group-toggle",
        toggleExpandedCssClass: "expanded",
        toggleCollapsedCssClass: "collapsed",
        enableExpandCollapse: true,
        groupFormatter: defaultGroupCellFormatter,
        totalsFormatter: defaultTotalsCellFormatter
    };
    options = $.extend(true, {}, _defaults, options);
    function defaultGroupCellFormatter(row, cell, value, columnDef, item) {
        if (!options.enableExpandCollapse) {
            return item.title;
        }
        var indentation = item.level * 15 + "px";
        return "<span class='" + options.toggleCssClass + " " +
            (item.collapsed ? options.toggleCollapsedCssClass : options.toggleExpandedCssClass) +
            "' style='margin-left:" + indentation + "'>" +
            "</span>" +
            "<span class='" + options.groupTitleCssClass + "' level='" + item.level + "'>" +
            item.title +
            "</span>";
    }
    function defaultTotalsCellFormatter(row, cell, value, columnDef, item) {
        return (columnDef.groupTotalsFormatter && columnDef.groupTotalsFormatter(item, columnDef)) || "";
    }
    function init(grid) {
        _grid = grid;
        _grid.onClick.subscribe(handleGridClick);
        _grid.onKeyDown.subscribe(handleGridKeyDown);
    }
    function destroy() {
        if (_grid) {
            _grid.onClick.unsubscribe(handleGridClick);
            _grid.onKeyDown.unsubscribe(handleGridKeyDown);
        }
    }
    function handleGridClick(e, args) {
        var item = this.getDataItem(args.row);
        if (item && item instanceof Slick.Group && $(e.target).hasClass(options.toggleCssClass)) {
            var range = _grid.getRenderedRange();
            this.getData().setRefreshHints({
                ignoreDiffsBefore: range.top,
                ignoreDiffsAfter: range.bottom + 1
            });
            if (item.collapsed) {
                this.getData().expandGroup(item.groupingKey);
            }
            else {
                this.getData().collapseGroup(item.groupingKey);
            }
            e.stopImmediatePropagation();
            e.preventDefault();
        }
    }
    function handleGridKeyDown(e) {
        if (options.enableExpandCollapse && (e.which == Slick.keyCode.SPACE)) {
            var activeCell = this.getActiveCell();
            if (activeCell) {
                var item = this.getDataItem(activeCell.row);
                if (item && item instanceof Slick.Group) {
                    var range = _grid.getRenderedRange();
                    this.getData().setRefreshHints({
                        ignoreDiffsBefore: range.top,
                        ignoreDiffsAfter: range.bottom + 1
                    });
                    if (item.collapsed) {
                        this.getData().expandGroup(item.groupingKey);
                    }
                    else {
                        this.getData().collapseGroup(item.groupingKey);
                    }
                    e.stopImmediatePropagation();
                    e.preventDefault();
                }
            }
        }
    }
    function getGroupRowMetadata(item) {
        return {
            selectable: false,
            focusable: options.groupFocusable,
            cssClasses: options.groupCssClass,
            columns: {
                0: {
                    colspan: "*",
                    formatter: options.groupFormatter,
                    editor: null
                }
            }
        };
    }
    function getTotalsRowMetadata(item) {
        return {
            selectable: false,
            focusable: options.totalsFocusable,
            cssClasses: options.totalsCssClass,
            formatter: options.totalsFormatter,
            editor: null
        };
    }
    return {
        init: init,
        destroy: destroy,
        getGroupRowMetadata: getGroupRowMetadata,
        getTotalsRowMetadata: getTotalsRowMetadata
    };
}
