import connectDB from "../index.js";

//All the users info (user_name, user_password, email, age, user_register)
export async function getUsersInf() {
    const db = await connectDB();
    const [res, fields] = await db.execute(`SELECT * FROM scollective.USER_INFO WHERE user_name = \'${user_name}\'`);
    db.end();
    return res;
}

//Get login info (username and password)
export async function getUser(user_name) {
    const db = await connectDB();
    const [res, fields] = await db.execute(`SELECT * FROM scollective.GETUSER WHERE user_name = \'${user_name}\'`);
    db.end();
    return res;
}

//Create User (user_name, user_password, email, age, user_register)
export async function addUser(user_name, user_password, email, age) {
    const db = await connectDB;

    const [res] = await db.execute(
        `INSERT INTO scollective.USER_INFO(user_name, user_password, email, age) VALUES(\'${user_name}\', \'${user_password}\', \'${email}\', \'${age}\')'`
    );

    db.end();
    return res
}

//Update User (user_password)
export async function updatePassword(data) {
    const db = await connectDB;
    const {user_password} = data;
    const [res] = await db.execute(
        `UPDATE scollective.USER_INFO SET user_password = ? WHERE user_name = \'${user_name}\ `,
        [user_password]
    );
    db.end();
    return res
}

 