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
import { ENV, PORT } from "./const.js";

import { createProxyMiddleware } from 'http-proxy-middleware';

import "dotenv/config"

const API_ENDPOINT = process.env.API_ENDPOINT;

const app = express();

const options = {
    target: API_ENDPOINT, // target host
    changeOrigin: true, // needed for virtual hosted sites
    pathRewrite: {
        [`^/api/users`]: '/users', // rewrite path 
        [`^/api/stats`]: '/stats', // rewrite path 
        [`^/api/progress`]: '/progress', // rewrite path 
        [`^/api/event`]: '/event', // rewrite path 
        [`^/api/gadget`]: '/gadget', // rewrite path 
    },
}

// extend the proxy depending on the endpoint
app.use('/api/users', createProxyMiddleware(options), usersRouter);
app.use('/api/stats', createProxyMiddleware(options), statsRouter);
app.use('/api/progress', createProxyMiddleware(options), progressRouter);
app.use('/api/event', createProxyMiddleware(options), eventRouter);
app.use('/api/gadget', createProxyMiddleware(options), gadgetRouter);

app.use(express.json());
app.use(cors());

//Ruta por defualt
app.get("/", (req, res) => {
    res.send(`Lo siento Octavio ya no tenemos el puerto expuesto 😈`);
});

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
