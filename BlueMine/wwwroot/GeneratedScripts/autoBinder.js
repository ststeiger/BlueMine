export function autoBind(self) {
    for (var _i = 0, _a = Object.getOwnPropertyNames(self.constructor.prototype); _i < _a.length; _i++) {
        var key = _a[_i];
        var val = self[key];
        if (key !== 'constructor' && typeof val === 'function') {
            self[key] = val.bind(self);
        }
    }
    return self;
}
