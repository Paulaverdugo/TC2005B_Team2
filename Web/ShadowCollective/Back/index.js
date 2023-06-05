import express from "express";
import bodyParser from "body-parser";
import {
    usersRouter,
    statsRouter,
    progressRouter,
} from "./routes/index.js";

import mysql from "mysql2/promise";
import {ENV, PORT} from "./const.js";


const app = express();

app.use(express.json());

//Ruta por defualt
app.get("/", (req, res) => {
    res.send(`Servidor trabajando en el puerto ${PORT}`);
});

// ----  Rutas ---- 
app.use("/users", usersRouter);
app.use("/stats", statsRouter);
app.use("/progress", progressRouter);

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
