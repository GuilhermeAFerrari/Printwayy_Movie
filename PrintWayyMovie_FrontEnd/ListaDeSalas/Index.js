function loadAllSalas() {
    getSession();

    axios.get('https://localhost:7098/salas').then(res => {
        var estruturaCorpoTabela = "";

        res.data.forEach(obj => {
            estruturaCorpoTabela += `<tr>`;
            Object.entries(obj).forEach(([key, value]) => {
                if(key != "id") estruturaCorpoTabela += `<td>${value}</td>`;
            });
            estruturaCorpoTabela += `</tr>`;
        });
        

        document.getElementById("bodyTable").innerHTML = estruturaCorpoTabela;
    });
}