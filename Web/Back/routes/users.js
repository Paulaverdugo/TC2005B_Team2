import { Router } from 'express';

const router = Router();

router.get("/:username", async (req, res) => {
    try {
        const {username} = req.params;
        const user = await getUser(username); //helper func
        res.status(200).json({
            msg: "usuario obtenido",
            data: user,
        });
    } catch (error) {
        console.log("Error de usurio: ", error);
        res.status(500).json({
            msg: "error perro",
            error,
        });
    }
});

export default router;

