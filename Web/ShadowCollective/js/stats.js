try 
{
    const users_response = await fetch('https://tc2005bteam2-production.up.railway.app/stats/', {
        method: 'GET',
        headers: {
            "Content-Type": "application/json",
            "Access-Control-Allow-Origin": "http://127.0.0.1:5500",
        }
    });

    const type_response = await fetch('https://tc2005bteam2-production.up.railway.app/stats/getTopTypeWins', {
        method: 'GET',
        headers: {
            "Content-Type": "application/json",
            "Access-Control-Allow-Origin": "http://127.0.0.1:5500",
        }
    });

    const dead_response = await fetch('https://tc2005bteam2-production.up.railway.app/stats/getTopTypedeads', {
        method: 'GET',
        headers: {
            "Content-Type": "application/json",
            "Access-Control-Allow-Origin": "http://127.0.0.1:5500",
        }
    });


    const gadget_response = await fetch('https://tc2005bteam2-production.up.railway.app/stats/getTopGadget', {
        method: 'GET',
        headers: {
            "Content-Type": "application/json",
            "Access-Control-Allow-Origin": "http://127.0.0.1:5500",
        }
    });


    console.log(users_response);

    if (users_response.ok) 
    {
        const results = await users_response.json()

        console.log(results)

        const values = Object.values(results);
        console.log(values);

        const names = values.map((value) => value.user_name);
        const wins = values.map((value) => value.user_wins);

        const ctx1 = document.getElementById('winsChart').getContext('2d');
        const chart1 = new Chart(ctx1, 
            {
                type: 'bar',
                data: {
                    labels: names,
                    datasets: [
                        {
                            label: '',
                            data: wins,
                            backgroundColor: ['rgba(255, 99, 132, 0.2)',],
                            borderColor: ['rgba(255, 99, 132, 1)',],
                            borderWidth: 1
                        }
                    ]
            },
        })
    }


    if (type_response.ok) 
    {

        const results = await type_response.json();

        console.log(results);

        const type_values = Object.values(results);
        console.log(type_values);

        const type_names = type_values.map((value) => value.name_ptypes);
        const type_wins = type_values.map((value) => value.player_type_wins);

        const ctx2 = document.getElementById('winsTypeChart').getContext('2d');

        const chart2 = new Chart(ctx2, 
            {
                type: 'pie',
                data: {
                    labels: type_names,
                    datasets: [
                        {
                            label: '',
                            data: type_wins,
                            backgroundColor: ['rgba(66, 106, 39)',],
                            borderColor: ['rgba(120, 207, 59)',],
                            borderWidth: 1
                        }
                    ]
            },
        })
    }



    if (dead_response.ok) 
    {

        const results = await dead_response.json();

        console.log(results);

        const dead_values = Object.values(results);
        console.log(dead_values);

        const dead_names = dead_values.map((value) => value.name_ptypes);
        const dead_type = dead_values.map((value) => value.player_type_deads);

        const ctx3 = document.getElementById('deadTypeChart').getContext('2d');

        const chart3 = new Chart(ctx3, 
            {
                type: 'bar',
                data: {
                    labels: dead_names,
                    datasets: [
                        {
                            label: '',
                            data: dead_type,
                            backgroundColor: ['rgba(140, 64, 176)',],
                            borderColor: ['rgba(140, 64, 176)',],
                            borderWidth: 1
                        }
                    ]
            },
        })
    }
    else{
        console.log("No esta actualizado")
    }



    if (gadget_response.ok) 
    {

        const results = await gadget_response.json();

        console.log(results);

        const gadget_values = Object.values(results);
        console.log(gadget_values);

        const gadget_names = gadget_values.map((value) => value.gadget_name);
        const gadget_frequency = gadget_values.map((value) => value.most_gadget);

        const ctx4 = document.getElementById('gadgetTypeChart').getContext('2d');

        const chart4 = new Chart(ctx4, 
            {
                type: 'pie',
                data: {
                    labels: gadget_names,
                    datasets: [
                        {
                            label: 'Wins',
                            data: gadget_frequency,
                            backgroundColor: ['rgba(255, 99, 132, 0.2)',],
                            borderColor: ['rgba(255, 99, 132, 1)',],
                            borderWidth: 1
                        }
                    ]
            },
        })
    }


} catch(e) {
    //Error
    console.log(e);
}


