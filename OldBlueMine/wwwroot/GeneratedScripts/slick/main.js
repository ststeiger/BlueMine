
import Slick from './slick.core.js';
import Editors from './slick.editors.js';
import Data from './slick.dataview.js';
import Grid from './slick.grid.js';
import FrozenGrid from './slick-frozen.grid.js';
import Formatters from './slick.formatters.js';
export { Slick, Editors, Formatters, Data, Grid, FrozenGrid };



const columns = [
    { id: 'title', name: 'Title', field: 'title', maxWidth: 100, minWidth: 80 },
    { id: 'duration', name: 'Duration', field: 'duration', resizable: false },
    { id: '%', name: '% Complete', field: 'percentComplete' },
    { id: 'start', name: 'Start', field: 'start' },
    { id: 'finish', name: 'Finish', field: 'finish' },
    { id: 'effort-driven', name: 'Effort Driven', field: 'effortDriven' }
];



const options = {
    enableCellNavigation: true,
    enableColumnReorder: false,
    forceFitColumns: !true,
    frozenColumn: 0,
    frozenRow: 1
};


let grid;


async function init(id)
{
    let data = [];

    for (let i = 0; i < 21; i++)
    {
        let d = (data[i] = {});

        d.id = i;
        d['title'] = 'Task ' + i;
        d['description'] = 'This is a sample task description.\n  It can be multiline';
        d['duration'] = '5 days';
        d['percentComplete'] = Math.round(Math.random() * 100);
        d['start'] = '01/01/2009';
        d['finish'] = '01/05/2009';
        d['effortDriven'] = (i % 5 == 0);
    }

    let mygrid = new Grid(id, data, columns, options);
    return mygrid;
}


async function main()
{
    grid = await init("#testTable");
}

main();
