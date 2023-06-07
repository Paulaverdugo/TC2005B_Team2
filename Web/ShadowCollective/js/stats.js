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
                            label: 'Wins',
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
                type: 'bar',
                data: {
                    labels: type_names,
                    datasets: [
                        {
                            label: 'Wins',
                            data: type_wins,
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


