
// this.autoBind(this);
export function autoBind(self: any)
{
    for (const key of Object.getOwnPropertyNames(self.constructor.prototype))
    {
        const val = self[key];

        if (key !== 'constructor' && typeof val === 'function')
        {
            // console.log(key);
            self[key] = val.bind(self);
        } // End if (key !== 'constructor' && typeof val === 'function') 

    } // Next key 

    return self;
} // End Function autoBind
