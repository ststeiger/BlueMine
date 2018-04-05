

export class foobar
{
    
    private m_mem:string;

    get foo():string
    {
        return this.m_mem;
    }

    set foo(value:string)
    {
        this.m_mem = value;
    }

}