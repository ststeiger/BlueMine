export default CellRangeDecorator;
function CellRangeDecorator(grid, options) {
    var _elem;
    var _defaults = {
        selectionCssClass: 'slick-range-decorator',
        selectionCss: {
            zIndex: '9999',
            border: '2px dashed red'
        }
    };
    options = Object.assign({}, _defaults, options);
    function show(range) {
        if (!_elem) {
            _elem = $('<div></div>', { css: options.selectionCss })
                .addClass(options.selectionCssClass)
                .css('position', 'absolute')
                .appendTo(grid.getCanvasNode());
        }
        var from = grid.getCellNodeBox(range.fromRow, range.fromCell);
        var to = grid.getCellNodeBox(range.toRow, range.toCell);
        _elem.css({
            top: from.top - 1,
            left: from.left - 1,
            height: to.bottom - from.top - 2,
            width: to.right - from.left - 2
        });
        return _elem;
    }
    function hide() {
        if (_elem) {
            _elem.remove();
            _elem = null;
        }
    }
    Object.assign(this, {
        show: show,
        hide: hide
    });
}
