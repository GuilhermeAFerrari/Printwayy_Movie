function loadAllUsuarios() {
    var email = document.getElementById("email").value;
    var senha = document.getElementById("senha").value;

    axios.get('https://localhost:7098/usuarios').then(res => {

        for(var x in res.data) {
            if(res.data[x].cl_Email == email && res.data[x].cl_Senha == senha) {
                if(email == "gerente@printwayy.com") {
                    sessionStorage.setItem('levelUser', 'gerente');
                }
                window.location.href = "../ListaDeSalas/Index.html";
            }
         }
        toastr.error('Usuário e/ou senha inválidos', 'Erro')

    });
}
