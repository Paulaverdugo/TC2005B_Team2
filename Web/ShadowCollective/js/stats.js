try {
    const users_response = await fetch('localhost:8000/stats/', {
        method: 'GET',
    });

    if (users_response.ok) {
        const result = await users_response.json();
        console.log(result);

        const values = Object.values(result);

        const names = values.map((value) => value.user_name);
        const wins = values.map((value) => value.user_wins);

        const ctx = document.getElementById('winsChart').getContext('2d');

        const chart = new Chart(ctx, 
            {
                type: 'bar',
                data: {
                    labels: names,
                    datasets: [{
                        label: 'Wins',
                        data: wins,
                        backgroundColor: [
                            'rgba(255, 99, 132, 0.2)',
                        ],
                        borderColor: [
                            'rgba(255, 99, 132, 1)',
                        ],
                        borderWidth: 1
                }]
            },
        })
    }
} catch(e) {
    // Handle error
}
