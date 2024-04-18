import React, {useState, useEffect, useCallback} from "react";
import { useSelector, useDispatch } from "react-redux";
import useStyles from "../shared/styles";
import { fileSelector } from "../../redux/file/fileSlice";
import { folderSelector } from "../../redux/folder/folderSlice";
import { Grid } from "@material-ui/core";
import * as fileAT from "../../redux/file/fileAsyncThunk";
import FileView from "./FileView";
import UpsertFile from "./UpsertFile";
import ConfirmDialog from "../shared/ConfirmDialog";
import { useTranslation } from "react-i18next";

export default function FileList({isUpload, onCancelUpload}){
    const dispatch = useDispatch();
    const files = useSelector(fileSelector);
    const folders = useSelector(folderSelector);
    const {openFolder} = folders;
    const {selectFile} = files;
    const classes = useStyles();
    const [upsertState, setUpsertState] = useState({open: false, isCreate: true, item: {}});
    const [openConfirmDelete, setOpenConfirmDelete] = useState(false);
    const { t } = useTranslation();

    const loadFile = useCallback((path) => {
        if(path === "")
        {
            dispatch(fileAT.getAllAsync());
        }
        else {
            dispatch(fileAT.getSubAsync(path));
        }
        // eslint-disable-next-line react-hooks/exhaustive-deps
    },[]);

    //load folder
    useEffect(()=>{
        loadFile(openFolder.path);
    },[openFolder, loadFile]);

    //refresh
    useEffect(()=>{
        if(folders.isRefresh){
            loadFile(openFolder.path);
        }
    },[folders.isRefresh, openFolder.path, loadFile]);

    useEffect(()=>{
        if(files.isRefresh){
            loadFile(openFolder.path);
        }
    },[files.isRefresh, openFolder.path, loadFile]);

    useEffect(()=>{
        if(isUpload){
            setUpsertState({
                open: true, 
                isCreate: true, 
                item: {
                    parent: openFolder.path, 
                    name: ""
                }
            });
        }
    },[isUpload, openFolder.path]);

    const handleUpdate=()=>{
        setUpsertState({
            open: true, 
            isCreate: false, 
            item: {
                path: selectFile.path, 
                name: selectFile.name
            }
        });
    }
    const handleDelete=()=>{
        setOpenConfirmDelete(true);
    }
    const handleConfirmDelete =()=>{
        dispatch(fileAT.removeAsync(selectFile.path));
        handleCloseDelete();
    }
    const handleCloseDelete=()=>{
        setOpenConfirmDelete(false);
    }
    return (
        <>
            {files.list.length > 0 && files.list.map(f => (
                <Grid item xs={1} key={f.name}>
                    <FileView data={f} className={classes.paper}
                        isSelected={f.name === selectFile.name}
                        onUpdate={handleUpdate}
                        onDelete={handleDelete}/>
                </Grid>
            ))}

            <UpsertFile open={upsertState.open} item={upsertState.item} isCreate={upsertState.isCreate} onClose={()=>{
                setUpsertState({...upsertState, open:false});
                onCancelUpload();
            }} />
            <ConfirmDialog
                title={t("confirm-delete")}
                message={t("are-you-sure-want-to-delete", {
                    itemName: selectFile.name,
                })}
                open={openConfirmDelete}
                handleClose={handleCloseDelete}
                handleConfirm={handleConfirmDelete}
            />
        </>
    );
}