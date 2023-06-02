

//Player Types info
export async function getPTypes() {
    const db = await connectDB();
    const [res, fields] = await db.execute("SELECT * FROM scollective.PLAYER_TYPES");
    db.end();
    return res;
}

//Gadgets info
export async function getGadget() {
    const db = await connectDB();
    const [res, fields] = await db.execute("SELECT * FROM scollective.Gadget");
    db.end();
    return res;
}
