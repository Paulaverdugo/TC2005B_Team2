import express from "express";
import bodyParser from "body-parser";
import {
    usersRouter,
    statsRouter,
    progressRouter,
    eventRouter,
    gadgetRouter,
} from "./routes/index.js";
import cors from "cors";

import mysql from "mysql2/promise";
import {ENV, PORT} from "./const.js";


const app = express();

app.use(express.json());
app.use(cors());

//Ruta por defualt
app.get("/", (req, res) => {
    res.send(`Lo siento Octavio ya no tenemos el puerto expuesto 😈`);
});

// ----  Rutas ---- 
app.use("/users", usersRouter);
app.use("/stats", statsRouter);
app.use("/progress", progressRouter);
app.use("/event", eventRouter);
app.use("/gadget", gadgetRouter);

// ----- Body Parser -----
app.use(bodyParser.json());
app.use(bodyParser.urlencoded({ extended: false }));

app.listen(PORT, () => {
    console.log(`Servidor iniciado en el puerto ${PORT}`);
});

//Connect to DB
export default async function connectDB() {
    return await mysql.createConnection(ENV);
}
