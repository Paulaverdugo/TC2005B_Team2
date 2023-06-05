import { Router } from 'express';

import {
    getUserProgress,
    getProgressGadget,
    addProgress
} from "../helpers/progress.js"

//Get User progress: getUserProgress
router.get("/:uprogress", async (req, res) => {
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
router.get("/:pgadget", async (req, res) => {
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
        const {level_achieved, user_name, player_type, life_points} = req.body;
        const data = await addProgress(level_achieved, user_name, player_type, life_points);
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

export default router;