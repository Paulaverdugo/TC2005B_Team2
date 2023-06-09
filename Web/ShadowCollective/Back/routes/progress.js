import { Router } from 'express';

import {
    getUserProgress,
    getProgressGadget,
    addProgress,
    updateLevel
} from "../helpers/progress.js"

const router = Router();

//Get User progress: getUserProgress
router.get("/user/:username", async (req, res) => {
    try {
        const {username} = req.params;
        const data = await getUserProgress(username);
        if (!data) {
            res.status(404).json({
                msg: "Not found"
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

//Get the user gadget: getProgressGadget
router.get("/gadget/:username", async (req, res) => {
    try {
        const {username} = req.params;
        const data = await getProgressGadget(username);
        if (!data) {
            res.status(404).json({
                msg: "Not found"
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

//Create a new progress: addProgress
router.post("/newProgress", async (req, res) => {
    try {
        const {level_achieved, user_name, player_type} = req.body;
        const data = await addProgress(level_achieved, user_name, player_type);
        if (!data) {
            res.status(404).json({
                msg: "Error in saving progress"
            });
            return;
        }
        res.status(200).json({
            msg: "Progress saved",
            data,
        });
    } catch (error) {
        console.log("Error: ", error);
        res.status(500).json({
            msg: "Error", error,
        });
    }
});

//Update level: updateLevel
router.patch("/updateLevel", async (req, res) => {
    try {
        const data = await updateLevel(req.body);
        if (!data) {
            res.status(404).json({
                msg: "Error in upadating level"
            });
            return;
        }
        res.status(200).json({
            msg: "Level updated",
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
