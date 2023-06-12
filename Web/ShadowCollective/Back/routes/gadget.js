import { Router } from 'express';

import {
    addChosenGadget,
    deleteChosenGadget
} from "../helpers/gadget.js"

const router = Router();

//Create a chosen gadget: addChosenGadget
router.post("/addChosenGadget", async (req, res) => {
    try {
        const {progress_id, gadget_id} = req.body;
        const data = await addChosenGadget(progress_id, gadget_id);
        if (!data) {
            res.status(404).json({
                msg: "Not found"
            });
            return;
        }
        res.status(200).json({
            msg: "chosen gadget created",
            data,
        });
    } catch (error) {
        console.log("Error: ", error);
        res.status(500).json({
            msg: "Error", error,
        });
    }
});  

//Delete a chosen gadget (progress_id, gadget_id)
router.delete("/deleteChosenGadget", async (req, res) => {
    try {
        const {progress_id, gadget_id} = req.body;
        const data = await deleteChosenGadget(progress_id, gadget_id);
        if (!data) {
            res.status(404).json({
                msg: "Not found"
            });
            return;
        }
        res.status(200).json({
            msg: "chosen gadget created",
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