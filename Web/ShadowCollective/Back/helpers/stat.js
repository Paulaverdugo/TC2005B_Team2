import connectDB from "../index.js";

//Top 3 users 
export async function getTopUsers() {
    const db = await connectDB();
    const [res, fields] = await db.execute(`SELECT * FROM scollective.WINNER_USER`);
    db.end();
    return res;
}

//Ranking of playertypes
export async function getTopPT() {
    const db = await connectDB();
    const [res, fields] = await db.execute(`SELECT * FROM scollective.WINNER_PLAYERTYPE`);
    db.end();
    return res;
}

//Top 3 users gadgets 
export async function getTopGadgets() {
    const db = await connectDB();
    const [res, fields] = await db.execute(`SELECT * FROM scollective.MOST_GADGET`);
    db.end();
    return res;
}