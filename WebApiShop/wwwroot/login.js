const emailForUpdate = document.querySelector("#userName");
const firstNameForUpdate = document.querySelector("#firstName");
const lastNameForUpdate = document.querySelector("#lastName");
const passwordForUpdate = document.querySelector("#password");


const register = async () => {

    const newUser = {
        Id: 0,
        Email: emailForUpdate.value,
        FirstName: firstNameForUpdate.value,
        LastName: lastNameForUpdate.value,
        Password: passwordForUpdate.value
    }

    try {
        const response = await fetch('https://localhost:44367/api/User', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(newUser)
        })
        if (response.ok) {
            alert("נרשמת בהצלחה!");
        }
        else
            alert("יש בעיה בהרשמה, נסה שוב..");
    }
    catch (error) {
        alert(error.message);
        console.error('Error occurred:', error);
    }
    
    
}


const loginUserEmail = document.querySelector("#loginUserName");
const loginUserPassword = document.querySelector("#loginUserPassword");

const login = async () => {
    const loginUser = {
        Email: loginUserEmail.value,
        FirstName: '',
        LastName: '',
        Password: loginUserPassword.value,
    }

    try {
        const response = await fetch('https://localhost:44367/api/User/login', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(loginUser)
        })
        if (response.ok && response.status !== 204) {
            const user = await response.json();
            sessionStorage.setItem('CurrentUser', JSON.stringify(user))
            window.location.href = "update.html"
        }
        else {
            alert("משתמש לא נמצא 😕");
            }
    }
    catch (error) {
        console.error('Error occurred:', error);
    }
}

const checkStrength = async () => {
    const password = passwordForUpdate.value;
    const passwordObj = {
        Pass: password,
        Strength: 0
    };
    try {
        const response = await fetch('https://localhost:44367/api/Password', {  ///checkPassword', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(passwordObj)
        })
        if (!response.ok) {
            throw new Error(`HTTP error! status${response.status}`)
        }
        const result = await response.json();
        const score = Number(result.strength);
        const bar = document.getElementById("progressBar");
        bar.value = score * 25;
    }
    catch (error) {
        alert("error")
        console.error('Error occurred:', error);
    }
}


