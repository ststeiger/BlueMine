
// jquery:
// css(elem: Element, unknown: any, x?:boolean): any;


// slick.dataview.ts
// function recalc(_items, UNUSED?)



// slick.grid.ts
interface ISlickGrid
{
    Grid:any;
}


// interact.d.ts
// interface Interactable { allowFrom(className:string):Interactable;

// jquery.d.ts
// closest(selector: JQuery.Selector, context: Element | JQuery.Selector): this;

// slick.grid.ts
//  jQueryNewWidthBehaviour = (<number><any>verArray[0] == 1 && <number><any>verArray[1] >= 8) || <number><any>verArray[0] >= 2;

// slick.grid.ts
//function queuePostProcessedRowForCleanup(cacheEntry, postProcessedRow, rowIdx, columnIdx?){

// slick.grid.ts
//queuePostProcessedRowForCleanup(zombieRowCacheFromLastMouseWheelEvent, zombieRowPostProcessedFromLastMouseWheelEvent, null);

/*
// slick.grid.ts & slick.frozen.grid.ts
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

// slick.grid.ts
interface IColumnDefaults  {
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



// slick-frozen.grid.ts
interface Frozen
{
    FrozenGrid: any;
}

// slick-frozen.grid.ts
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





interface Math
{
    hypot: any;
    
}


interface Document
{
    selection: any;
}



interface  Window
{
    wrap: any;
}

interface  Function
{
    displayName: string;
}


interface StyleSheet
{
    owningElement: any;
}



interface  abc
{
    isFilterNarrowing: boolean;
    isFilterExpanding:boolean;
    isFilterUnchanged: boolean;
}


// Data
// GroupItemMetadataProvider()
