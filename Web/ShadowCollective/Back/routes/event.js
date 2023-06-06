import { Router } from 'express';
import {
    addWin,
    addDeads
} from "../helpers/users.js"

const router = Router();

//Create Win: addWin
router.post("/addWin", async (req, res) => {
    try {
        const {user_name, player_type} = req.body;
        const data = await addWin(user_name, player_type);
        if (!data) {
            res.status(404).json({
                msg: "Not found"
            });
            return;
        }
        res.status(200).json({
            msg: "Win created",
            data,
        });
    } catch (error) {
        console.log("Error: ", error);
        res.status(500).json({
            msg: "Error", error,

        });
    }
});

//Create Deads: addDeads
router.post("/addDeads", async (req, res) => {
    try {
        const {user_name, player_type, level_dead} = req.body;
        const data = await addDeads(user_name, player_type, level_dead);
        if (!data) {
            res.status(404).json({
                msg: "Not found"
            });
            return;
        }
        res.status(200).json({
            msg: "Dead created",
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