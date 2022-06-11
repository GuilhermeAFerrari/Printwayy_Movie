function loadAllFilmes() {
  getSession();
  
    axios.get('https://localhost:7098/filmes').then(res => {
        var estruturaCorpoTabela = "";

        for(var x in res.data) {
            estruturaCorpoTabela += `<tr>
                                        <td>
                                            <a onclick="deleteFilme(${res.data[x].id})">
                                                <img src="../assets/img/trash.svg">
                                            </a>
                                            &nbsp;&nbsp;&nbsp;
                                            <a onclick="carregarEditarFilme(${res.data[x].id})">
                                                <img src="../assets/img/pencil.svg">
                                            </a>
                                        </td>
                                        <td onmouseenter="loadImagem(${res.data[x].id})"
                                            onmouseout="hideDivs()">
                                            <img src="../assets/img/image-filme.svg">
                                        </td>
                                        <td>
                                            ${res.data[x].cl_Titulo}
                                        </td>
                                        <td onmouseenter="loadDescricao(${res.data[x].id})"
                                            onmouseout="hideDivs()" style="width: 14rem;">
                                            <div id="tdDescricao" >${res.data[x].cl_Descricao}</div>
                                        </td>
                                        <td>
                                            ${res.data[x].cl_Duracao}
                                        </td>
                                    </tr>`;
         }

        document.getElementById("bodyTable").innerHTML = estruturaCorpoTabela;
    });
}

function hideDivs() {
    document.getElementById("imagemFilme").style.display = "none";  
    document.getElementById("descricaoFilme").style.display = "none";  
}

function loadImagem(id) {
    axios.get(`https://localhost:7098/filmes/${id}`)
      .then((response) => {
        document.getElementById("imagemFilme").style.display = "flex";
        document.getElementById("imagemFilmeDiv").src = "../assets/banners/" + response.data.cl_Imagem;
      }, (error) => {
        toastr.error(error.response.data);
      });
}

function loadDescricao(id) {
    axios.get(`https://localhost:7098/filmes/${id}`)
      .then((response) => {
        document.getElementById("descricaoFilme").style.display = "flex";
        document.getElementById("descricaoFilmeDiv").innerHTML = response.data.cl_Descricao
      }, (error) => {
        toastr.error(error.response.data);
      });
}

function carregarEditarFilme(id) {
    window.location.href = `./ViewAddEdit.html?id=${id}`;
}

function loadFilme(id) {
  axios.get(`https://localhost:7098/filmes/${id}`)
      .then((response) => {
        document.getElementById("titulo").value = response.data.cl_Titulo;
        document.getElementById("descricao").value = response.data.cl_Descricao;
        document.getElementById("duracao").value = response.data.cl_Duracao;
      }, (error) => {
      toastr.error(error.response.data);
  });
}

function adicionarFilme() {
    var imagem = document.getElementById("imagem").value;
    !imagem ? imagem = null : imagem = document.getElementById("imagem").files[0].name;
    var titulo = document.getElementById("titulo").value;
    var descricao = document.getElementById("descricao").value;
    var duracao = document.getElementById("duracao").value;

    axios.post('https://localhost:7098/create-filme', {
        Cl_Imagem: imagem,
        Cl_Titulo: !titulo ? null : titulo,
        Cl_Descricao: !descricao ? null : descricao,
        Cl_Duracao: !duracao ? null : duracao
      })
      .then((response) => {
        toastr.success('Filme adicionado com sucesso');
        setTimeout(() => {
            window.location.href = "./Index.html";
        }, 1500);
      }, (error) => {
        toastr.error(error.response.data);
      });
}

function editarFilme(id) {
    var imagem = document.getElementById("imagem").value;
    !imagem ? imagem = null : imagem = document.getElementById("imagem").files[0].name;
    var titulo = document.getElementById("titulo").value;
    var descricao = document.getElementById("descricao").value;
    var duracao = document.getElementById("duracao").value;

    axios.put(`https://localhost:7098/update-filme/${id}`, {
        id: id,
        Cl_Imagem: imagem,
        Cl_Titulo: !titulo ? null : titulo,
        Cl_Descricao: !descricao ? null : descricao,
        Cl_Duracao: !duracao ? null : duracao
      })
      .then(() => {
        toastr.success('Filme editado com sucesso');
        setTimeout(() => {
            window.location.href = "./Index.html";
        }, 1500);
      }, (error) => {
        toastr.error(error.response.data);
      });
}

function deleteFilme(id) {
    var confirmacao = confirm("Deseja realmente remover?");
    if(confirmacao == true) {
        axios.delete(`https://localhost:7098/delete-filme/${id}`)
            .then((res) => {
                toastr.success(`Filme ${res.data.cl_Titulo} removido com sucesso`);
                loadAllFilmes();
            }, (error) => {
                toastr.error(error.response.data);
            }
        );
    }
}

function getParams() {
  var params = new URLSearchParams(window.location.search);
  var id = params.has("id");

  if(id) {
    id = params.get("id");
    document.getElementById("button-salvar").setAttribute("onclick", `editarFilme(${id})`);
    loadFilme(id);
  }
}