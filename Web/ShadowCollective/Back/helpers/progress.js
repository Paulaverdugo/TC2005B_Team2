import connectDB from "../index.js";


//All the users info from progress (ID, level_achieved, user_name, player_type)
//Gives the last progress id from the user
export async function getUserProgress(user_name) {
    const db = await connectDB();
    const [res, fields] = await db.execute(
        `SELECT * FROM scollective.PROGRESS 
        WHERE scollective.PROGRESS.id_progress = (SELECT MAX(id_progress) FROM scollective.PROGRESS 
        WHERE user_name = \'${user_name}\')
        AND level_achieved <> 4`
    );
    db.end();
    return res;
}

//Get the chosen gadget id on the last progress from the user
export async function getProgressGadget(user_name) {
    const db = await connectDB();
    const [res, fields] = await db.execute(
        `SELECT gadget_id FROM scollective.CHOSEN_GADGET
        INNER JOIN  scollective.PROGRESS ON scollective.CHOSEN_GADGET.progress_id = scollective.PROGRESS.id_progress
        WHERE scollective.PROGRESS.id_progress = (SELECT MAX(id_progress) FROM scollective.PROGRESS 
        WHERE user_name = \'${user_name}\')`
    );
    db.end();
    return res;
}

//Create a new progress (level_achieved, user_name, player_type)
export async function addProgress(level_achieved, user_name, player_type) {
    const db = await connectDB();

    const [res] = await db.execute(
        `INSERT INTO scollective.PROGRESS(level_achieved, user_name, player_type) VALUES(\'${level_achieved}\', \'${user_name}\', \'${player_type}\')`
    );

    db.end();
    return res
}
