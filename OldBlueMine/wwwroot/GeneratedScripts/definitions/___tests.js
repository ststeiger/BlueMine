var foobar = (function () {
    function foobar() {
    }
    Object.defineProperty(foobar.prototype, "foo", {
        get: function () {
            return this.m_mem;
        },
        set: function (value) {
            this.m_mem = value;
        },
        enumerable: true,
        configurable: true
    });
    return foobar;
}());
export { foobar };
