function redirectToLogin() {
    setTimeout(() => {
        window.location.href = "./Login/Index.html";
    }, 3000);
}

function getSession() {
    var levelUser = sessionStorage.getItem('levelUser');
  
    if(levelUser == "gerente") {
        document.getElementById("typeUser").innerHTML = levelUser.toUpperCase();
    }
    else {
        var buttonAdd = document.getElementById("buttonAdd");
        if (buttonAdd != null)
            buttonAdd.style.display = "none";
        
    }
}

function sair() {
    var confirmacao = confirm("Deseja realmente sair?");
    if(confirmacao == true) {
        sessionStorage.clear();
        window.location.href = "../Login/Index.html";
    }
}