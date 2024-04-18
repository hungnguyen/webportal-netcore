import React, {useCallback, useEffect, useState} from "react";
import { useSelector, useDispatch } from "react-redux";
import { useTranslation } from "react-i18next";
import useStyles from "../shared/styles";
import * as folderAT from "../../redux/folder/folderAsyncThunk";
import { folderSelector } from "../../redux/folder/folderSlice";
import FolderView from "./FolderView";
import UpsertFolder from "./UpsertFolder";
import ConfirmDialog from "../shared/ConfirmDialog";
import { Grid } from "@material-ui/core";

export default function FolderList({isCreate, onCancelCreate}){
    const dispatch = useDispatch();
    const { t } = useTranslation();
    const classes = useStyles();

    const [upsertState, setUpsertState] = useState({open: false, isCreate: true, item: {}});
    const [openConfirmDelete, setOpenConfirmDelete] = useState(false);
    const folders = useSelector(folderSelector);
    const {openFolder, selectFolder} = folders;

    const loadFolder = useCallback((path) => {
        if(path === "")
        {
            dispatch(folderAT.getAllAsync());
        }
        else {
            dispatch(folderAT.getSubAsync(path));
        }
        // eslint-disable-next-line react-hooks/exhaustive-deps
    },[]);

    //load folder
    useEffect(()=>{
        loadFolder(openFolder.path);
    },[openFolder, loadFolder]);

    //refresh
    useEffect(()=>{
        if(folders.isRefresh){
            loadFolder(openFolder.path);
        }
    },[folders.isRefresh, loadFolder, openFolder.path]);

    useEffect(()=>{
        if(isCreate){
            setUpsertState({
                open: true, 
                isCreate: true, 
                item: {
                    parent: openFolder.path, 
                    name: ""
                }
            });
        }
    },[isCreate, openFolder.path]);

    const handleUpdate=()=>{
        setUpsertState({
            open: true, 
            isCreate: false, 
            item: {
                path: selectFolder.path, 
                name: selectFolder.name
            }
        });
    }
    const handleDelete=()=>{
        setOpenConfirmDelete(true);
    }
    const handleConfirmDelete =()=>{
        dispatch(folderAT.removeAsync(selectFolder.path));
        handleCloseDelete();
    }
    const handleCloseDelete=()=>{
        setOpenConfirmDelete(false);
    }
    
    return (
        <>
            {folders.list.length > 0 && folders.list.map(f => (
                <Grid item xs={1} key={f.name}>
                    <FolderView data={f} className={classes.paper} 
                        isSelected={f.name === selectFolder.name}
                        onUpdate={handleUpdate}
                        onDelete={handleDelete}/>
                </Grid>
            ))}

            <UpsertFolder open={upsertState.open} item={upsertState.item} isCreate={upsertState.isCreate} onClose={()=>{
                setUpsertState({...upsertState, open:false});
                onCancelCreate();
                }} />
            <ConfirmDialog
                title={t("confirm-delete")}
                message={t("are-you-sure-want-to-delete", {
                    itemName: selectFolder.name,
                })}
                open={openConfirmDelete}
                handleClose={handleCloseDelete}
                handleConfirm={handleConfirmDelete}
            />
        </>
    );
}