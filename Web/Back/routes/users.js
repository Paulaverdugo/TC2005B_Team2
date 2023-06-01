import { Router } from 'express';
import {
    getUsersInf,
    getUser,
    addUser
} from "../helpers/users.js"

const router = Router();

//All the users info (user_name, user_password, email, age, user_register): getUsersInf()
router.get("/", async (req, res) => {
    try {
        const data = await getUsersInf();
        if (!data) {
            res.status(404).json({
                msg: "No se encontró la información solicitada"
            });
            return;
        }
        res.status(200).json(data);
    } catch (error) {
        console.log("Error: ", error);
        res.status(500).json({
            msg: "Error", error,
        });
    }
});


//Get login info (username and password): getUser
router.get("/:username", async (req, res) => {
    try {
        const {username} = req.params;
        const data = await getUser(username);
        if (!data) {
            res.status(404).json({
                msg: "No se encontró la información solicitada"
            });
            return;
        }
        res.status(200).json(data);
    } catch (error) {
        console.log("Error: ", error);
        res.status(500).json({
            msg: "Error", error,
        });
    }
});


//Create User (user_name, user_password, email, age, user_register): addUser
router.post("/createUser", async (req, res) => {
    try {
        const {user_name, user_password, email, age} = req.body;
        const data = await addUser(user_name, user_password, email, age);
        if (!data) {
            res.status(404).json({
                msg: "No se creó el usario"
            });
            return;
        }
        res.status(200).json({
            msg: "Usuario creado de manera exitosa",
            data,
        });
    } catch (error) {
        console.log("Error: ", error);
        res.status(500).json({
            msg: "Error", error,

        });
    }
});

export default router;

