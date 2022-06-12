function loadAllSessoes() {
    getSession();
    
    axios.get('https://localhost:7098/sessoes').then(res => {
        var estruturaCorpoTabela = "";

        for(var x in res.data) {
            estruturaCorpoTabela += `<tr>
                                        <td>
                                            <a onclick="deleteSessao(${res.data[x].id})">
                                                <img src="../assets/img/trash.svg">
                                            </a>
                                        </td>
                                        <td>
                                            ${res.data[x].cl_Data}
                                        </td>
                                        <td>
                                            ${res.data[x].cl_HoraInicio}
                                        </td>
                                        <td>
                                            ${res.data[x].cl_HoraFim}
                                        </td>
                                        <td>
                                            ${res.data[x].cl_Valor}
                                        </td>
                                        <td>
                                            ${res.data[x].cl_Animacao}
                                        </td>
                                        <td>
                                            ${res.data[x].cl_Audio}
                                        </td>
                                        <td>
                                            ${res.data[x].nomeFilme}
                                        </td>
                                        <td>
                                            ${res.data[x].nomeSala}
                                        </td>
                                    </tr>`;
         }
        
        document.getElementById("bodyTable").innerHTML = estruturaCorpoTabela;
    });
}

function deleteSessao(id) {
    var levelUser = sessionStorage.getItem('levelUser');
    if(levelUser != 'gerente') {
      toastr.error("Necessário nível de login como Gerente para essa ação")
    }
    else {
        var confirmacao = confirm("Deseja realmente remover?");
        if(confirmacao == true) {
            axios.delete(`https://localhost:7098/delete-sessao/${id}`)
                .then(() => {
                    toastr.success('Sessão removida com sucesso');
                    loadAllSessoes();
                }, (error) => {
                    toastr.error(error.response.data);
                }
            );
        }
    }
}

function adicionarSessao() {
    var data = document.getElementById("data").value;
    var horaInicio = document.getElementById("horaInicio").value;
    var valor = document.getElementById("valor").value;
    var animacao = document.getElementById("animacao").value;
    var audio = document.getElementById("audio").value;
    var idFilme = document.getElementById("filme").value;
    var idSala = document.getElementById("sala").value;

    axios.post('https://localhost:7098/create-sessao', {
        Cl_Data: !data ? null : data,
        Cl_HoraInicio: !horaInicio ? null : horaInicio,
        Cl_Valor: !valor ? 0 : valor,
        Cl_Animacao: animacao,
        Cl_Audio: audio,
        Fk_IdFilme: idFilme,
        Fk_IdSala: idSala
      })
      .then(() => {
        toastr.success('Sessão adicionada com sucesso');
        setTimeout(() => {
            window.location.href = "./Index.html";
        }, 1500);
      })
      .catch(error => {
        toastr.error(error.response.data);
      });
}

function loadFilmesSalas() {
    axios.get('https://localhost:7098/filmes').then(res => {
        var estruturaSelectFilmes = document.getElementById("filme");

        for(var x in res.data) {
            var newOption = document.createElement('option');
            newOption.value = res.data[x].id;
            newOption.innerHTML = res.data[x].cl_Titulo;
            estruturaSelectFilmes.appendChild(newOption);
         }
    });

    axios.get('https://localhost:7098/salas').then(res => {
        var estruturaSelectSalas = document.getElementById("sala");

        for(var x in res.data) {
            var newOption = document.createElement('option');
            newOption.value = res.data[x].id;
            newOption.innerHTML = res.data[x].cl_Nome;
            estruturaSelectSalas.appendChild(newOption);
         }
    });
}