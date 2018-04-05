
// fix jquery.d.ts
// css(elem: Element, unknown: any, x?:boolean): any;

// fix jquery.d.ts
// closest(selector: JQuery.Selector, context: Element | JQuery.Selector): this;

// fix interact.d.ts
// interface Interactable { allowFrom(className:string):Interactable;

// fix for flatpickr
interface ObjectConstructor
{
    assign:any;
}


// fix slick.grid.ts
interface ISlickGrid
{
    Grid:any;
}



// fix slick.grid.ts
// jQueryNewWidthBehaviour = (<number><any>verArray[0] == 1 && <number><any>verArray[1] >= 8) || <number><any>verArray[0] >= 2;

// fix slick.grid.ts
// function queuePostProcessedRowForCleanup(cacheEntry, postProcessedRow, rowIdx, columnIdx?){

// fix slick.grid.ts
// queuePostProcessedRowForCleanup(zombieRowCacheFromLastMouseWheelEvent, zombieRowPostProcessedFromLastMouseWheelEvent, null);

// fix slick.grid.ts
interface IColumnDefaults
{
    name: string;
    resizable: boolean;
    sortable: boolean;
    minWidth: number;
    rerenderOnResize: boolean;
    headerCssClass: string;
    defaultSortAsc: boolean;
    focusable: boolean;
    selectable: boolean;
}

/*
// fix slick.grid.ts & slick.frozen.grid.ts
function toggleCellClass($cell, times, speed)
{
    if (!times){
        return;
    }
    setTimeout(function(){
            $cell.queue(function(){
                $cell.toggleClass(options.cellFlashingCssClass).dequeue();
                toggleCellClass(times - 1);
            });
        },
        speed);
}

function flashCell(row, cell, speed){
*/


// fix slick-frozen.grid.ts
interface Frozen
{
    FrozenGrid: any;
}

// fix slick-frozen.grid.ts
interface IColumnDefaults
{
    width?: number; // missing

    name: string;
    resizable: boolean;
    sortable: boolean;
    minWidth: number;
    rerenderOnResize: boolean;
    headerCssClass: string;
    defaultSortAsc: boolean;
    focusable: boolean;
    selectable: boolean;
}



// slick.dataview.ts
// GroupItemMetadataProvider()

// fix slick.dataview.ts
// function recalc(_items, UNUSED?)

// fix slick.dataview.ts
interface IRefreshHint
{
    isFilterNarrowing: boolean;
    isFilterExpanding: boolean;
    isFilterUnchanged: boolean;
    ignoreDiffsBefore: number;
    ignoreDiffsAfter: number;
}



// fix slick.editors.ts 
interface IEditor
{
    Editors: any;
}


/*
// Doesn't work - use FunctionStatics instead
declare var FloatEditor: {
    // zEmbed can queue functions to be invoked when the asynchronous script has loaded.
    //( callback: (args:any) => void ) : void;
    
    // ... and, once the asynchronous zEmbed script is loaded, the zEmbed object will
    // expose the widget API.
    // activate(): void;
    // hide(): void;
    // identify(): void;
    // setHelpCenterSuggestions(): void;
    // show(): void;
    DefaultDecimalPlaces: number;
};
*/

// slick.editors.ts
interface FunctionStatics
{
    DefaultDecimalPlaces: number;
}






// Fix: other strange stuff 

interface Window
{
    wrap: any;
}

interface Document
{
    selection: any;
}

interface StyleSheet
{
    owningElement: any;
}

interface Function
{
    displayName: string;
}

interface Math
{
    hypot: any;
}
