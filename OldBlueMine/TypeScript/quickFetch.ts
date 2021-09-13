
var url = "somedata.json";
var data = { key1: "value1", key2: "value2" };
/*



fetch(url)
  .then(function(data) {
    // Here you get the data to modify as you please
    })
  })
  .catch(function(error) {
    // If there is any error you will catch them here
  });



// -----------------------------------------


fetch(url,
    {
        method: "POST",
        body: JSON.stringify(data),
        headers: {
            "Content-Type": "application/json"
        },
        credentials: "same-origin"
    }
)
.then(function (response)
{
    
    console.log(
        response.status     //=> number 100–599
        , response.statusText //=> String
        , response.headers    //=> Headers
        , response.url        //=> String
    );
    

    // console.log(response.json());
    console.log(response.text());

    
    return response.text()
}
, function (error)
{
    console.log(
        error.message //=> String
    );
}
);
*/


async function foo()
{
    const rawResponse = await fetch('https://httpbin.org/post', {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ a: 1, b: 'Textual content' })
    });

    const content = await rawResponse.json();
    console.log(content);
}



let g: Response;
let exa: any;


// https://developer.mozilla.org/en-US/docs/Web/API/Fetch_API/Using_Fetch
async function getData(): Promise<Response>
{
    let f: Response;

    try
    {
        f = await fetch(url + "1",
            {
                method: "GET",
                headers: {
                    "Content-Type": "application/json"
                },
                credentials: "same-origin" // send credentials if the request URL is on the same origin as the calling script
                // credentials: "include" // To cause browsers to send a request with credentials included, even for a cross-origin call 
            }
        );

        if (f.status === 404)
        {
            console.log(f.statusText);
        }

        let json = await f.json();
        console.log(json);
        g = json;

        // console.log(f);
        // let arr = await g.body.getReader().read();
        // let text = await g.text();

        // g[0].place_id
    }
    catch (ex)
    {
        exa = ex;
        console.log("ex", ex);
    }

    return f;
}

let d = getData();
