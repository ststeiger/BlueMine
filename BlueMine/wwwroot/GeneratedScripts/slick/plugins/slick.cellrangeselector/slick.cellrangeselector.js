import Slick from '../../slick.core.js';
import CellRangeDecorator from '../slick.cellrangedecorator/slick.cellrangedecorator.js';
export default CellRangeSelector;
function CellRangeSelector(options) {
    var _grid;
    var _canvas;
    var _dragging;
    var _decorator;
    var _range = {};
    var _self = this;
    var _handler = new Slick.EventHandler();
    var _defaults = {
        selectionCss: {}
    };
    function init(grid) {
        options = Object.assign({}, _defaults, options);
        _decorator = new CellRangeDecorator(grid, options);
        _grid = grid;
        _canvas = _grid.getCanvasNode();
        _handler
            .subscribe(_grid.onDragInit, handleDragInit)
            .subscribe(_grid.onDragStart, handleDragStart)
            .subscribe(_grid.onDrag, handleDrag)
            .subscribe(_grid.onDragEnd, handleDragEnd);
    }
    function destroy() {
        _handler.unsubscribeAll();
    }
    function handleDragInit(e, dd) {
        e.stopImmediatePropagation();
    }
    function handleDragStart(jqueryEvent, interactEvent) {
        var cell = _grid.getCellFromEvent(interactEvent.originalEvent);
        if (_self.onBeforeCellRangeSelected.notify(cell) !== false) {
            if (_grid.canCellBeSelected(cell.row, cell.cell)) {
                _dragging = true;
            }
        }
        if (!_dragging) {
            return;
        }
        _grid.focus();
        var start = _grid.getCellFromPoint(interactEvent.x0 - $(_canvas).offset().left, interactEvent.y0 - $(_canvas).offset().top);
        _range = {
            start: start,
            end: {}
        };
    }
    function handleDrag(e, interactEvent) {
        if (!_dragging) {
            return;
        }
        var end = _grid.getCellFromPoint(interactEvent.pageX - $(_canvas).offset().left, interactEvent.pageY - $(_canvas).offset().top);
        if (!_grid.canCellBeSelected(end.row, end.cell)) {
            return;
        }
        _range.end = end;
        _decorator.show(new Slick.Range(_range.start.row, _range.start.cell, end.row, end.cell));
    }
    function handleDragEnd(e) {
        if (!_dragging) {
            return;
        }
        _dragging = false;
        _decorator.hide();
        _self.onCellRangeSelected.notify({
            range: new Slick.Range(_range.start.row, _range.start.cell, _range.end.row, _range.end.cell)
        });
        _range = {};
    }
    Object.assign(this, {
        init: init,
        destroy: destroy,
        onBeforeCellRangeSelected: new Slick.Event(),
        onCellRangeSelected: new Slick.Event()
    });
}
