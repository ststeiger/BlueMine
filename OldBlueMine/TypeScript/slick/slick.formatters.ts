function PercentCompleteFormatter(row, cell, value, columnDef, dataContext)
{
    if (value == null || value === '')
    {
        return '-';
    } else if (value < 50)
    {
        return `<span style='color:red;font-weight:bold;'>${value}%</span>`;
    } else
    {
        return `<span style='color:green'>${value}%</span>`;
    }
}

function PercentCompleteBarFormatter(row, cell, value, columnDef, dataContext)
{
    if (value == null || value === '')
    {
        return '';
    }

    let color;

    if (value < 30)
    {
        color = 'red';
    } else if (value < 70)
    {
        color = 'silver';
    } else
    {
        color = 'green';
    }

    return "<span class='percent-complete-bar' style='background:" + color + ';width:' + value + "%'></span>";
}

function YesNoFormatter(row, cell, value, columnDef, dataContext)
{
    return value ? 'Yes' : 'No';
}

function CheckmarkFormatter(row, cell, value, columnDef, dataContext)
{
    return value ? '✔' : '';
}

export default {
    PercentComplete: PercentCompleteFormatter,
    PercentCompleteBar: PercentCompleteBarFormatter,
    YesNo: YesNoFormatter,
    Checkmark: CheckmarkFormatter
};
