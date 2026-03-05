const welcomeText = document.querySelector("#currentUser")

let currentUser = JSON.parse(sessionStorage.getItem('CurrentUser'))

if (!currentUser) {
    alert("אין משתמש מחובר, בבקשה התחבר מחדש");
    window.location.href = "Home.html";
}

welcomeText.textContent = "Hello To " + currentUser.firstName;

const emailForUpdate = document.querySelector("#userName");
const firstNameForUpdate = document.querySelector("#firstName");
const lastNameForUpdate = document.querySelector("#lastName");
const passwordForUpdate = document.querySelector("#password");

emailForUpdate.value = currentUser.email;
firstNameForUpdate.value = currentUser.firstName;
lastNameForUpdate.value = currentUser.lastName;
passwordForUpdate.value = currentUser.password;


const update = async () => {

    const updateUser = {
        Id: currentUser.UserId,
        Email: emailForUpdate.value,
        FirstName: firstNameForUpdate.value,
        LastName: lastNameForUpdate.value,
        Password: passwordForUpdate.value
    }

    try {
        const response = await fetch(`https://localhost:44367/api/User/${currentUser.userId}`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(updateUser)
        });

        if (response.status == 400) {
            alert("העדכון לא הצליח, הסיסמא לא חזקה מספיק")
        }
        else {
            alert("הנתונים התעדכנו")
        }
    }
    catch (error) {
        console.error('Error occurred:', error);
    }

}