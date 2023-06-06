import connectDB from "../index.js";


//Create a chosen gadget (progress_id, gadget_id)
export async function addChosenGadget(progress_id, gadget_id) {
    const db = await connectDB();

    const [res] = await db.execute(
        `INSERT INTO scollective.CHOSEN_GADGET(progress_id, gadget_id) VALUES(\'${progress_id}\', \'${gadget_id}\')`
    );

    db.end();
    return res
}


//Delete a chosen gadget (progress_id, gadget_id)
export async function deleteChosenGadget(progress_id, gadget_id) {
    const db = await connectDB();

    const [res] = await db.execute(
        `DELETE FROM scollective.CHOSEN_GADGET WHERE progress_id = \'${progress_id}\' AND gadget_id = \'${gadget_id}\'`
    );

    db.end();
    return res
}