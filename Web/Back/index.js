import express from "express";
import bodyParser from "body-parser";
import cors from "cors";
import {
    usersRouter,
} from "./routes/index.js";

const app = express();

app.use(express.json());
app.use(cors());

//Ruta por defualt
app.get("/", (req, res) => {
    res.send("Servidor trabajando en el puerto 8000");
});

// ----  Rutas ---- 

app.use("/users", usersRouter);

// ----- Body Parser -----
app.use(bodyParser.json());
app.use(bodyParser.urlencoded({ extended: false }));

app.listen(8000, () => {
    console.log("Servidor iniciado en el puerto 8000");
});
