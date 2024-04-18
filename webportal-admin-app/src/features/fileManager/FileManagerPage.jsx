import React, { useState } from "react";
import { Typography, Grid, Paper, Toolbar, IconButton, Tooltip, Divider  } from "@material-ui/core";
import { Refresh, ArrowBack, CreateNewFolderOutlined, CloudUploadOutlined } from "@material-ui/icons";
import { useTranslation } from "react-i18next";
import useStyles from "../shared/styles";
import { useSelector, useDispatch } from "react-redux";
import { fileSelector } from "../../redux/file/fileSlice";
import { folderSelector, open, setIsRefresh } from "../../redux/folder/folderSlice";
import FolderList from "./FolderList";
import FileList from "./FileList";

export default function FileManagerPage(){
    const { t } = useTranslation();
    const classes = useStyles();

    const folders = useSelector(folderSelector);
    const {openFolder} = folders;
    const files = useSelector(fileSelector);
    const dispatch = useDispatch();
    const [isCreate, setIsCreate] = useState(false);
    const [isUpload, setIsUpload] = useState(false);
    
    const handleGoBack = () => {
        dispatch(open({
            path: openFolder.parent,
            parent: getParentPath(openFolder.parent)
        }));
    }

    const handleRefresh = () =>{
        dispatch(setIsRefresh(true));
    }

    const getParentPath = (path) => {
        if (!path) return;
        let arrStr = path.split('\\');
        arrStr.pop();
        return arrStr.join('\\');
    }

    return <>
        <Typography variant="h4" gutterBottom>
            {t("fileManager")}
        </Typography>
        
        <Paper className={classes.tablePaper}>
            <Toolbar className={classes.toolbar}>
                <Tooltip title={t("goBack")}>
                    <IconButton
                        aria-label={t("goBack")}
                        className={classes.margin}
                        onClick={handleGoBack}
                        disabled={openFolder.path === ""}
                    >
                        <ArrowBack fontSize="inherit" />
                    </IconButton>
                </Tooltip>
                <Tooltip title={t("refresh-lish")}>
                    <IconButton
                        aria-label={t("refresh-lish")}
                        className={classes.margin}
                        onClick={handleRefresh}
                    >
                        <Refresh fontSize="inherit" />
                    </IconButton>
                </Tooltip>
                <Tooltip title={t("createNewFolder")}>
                    <IconButton
                        aria-label={t("createNewFolder")}
                        className={classes.margin}
                        onClick={()=>setIsCreate(true)}
                    >
                        <CreateNewFolderOutlined fontSize="inherit" />
                    </IconButton>
                </Tooltip>
                <Tooltip title={t("uploadFile")}>
                    <IconButton
                        aria-label={t("uploadFile")}
                        className={classes.margin}
                        onClick={()=>setIsUpload(true)}
                    >
                        <CloudUploadOutlined fontSize="inherit" />
                    </IconButton>
                </Tooltip>
            </Toolbar>
            <Divider />
            <Grid container spacing={2}>
                <FolderList isCreate={isCreate} onCancelCreate={()=>setIsCreate(false)}/>
                <FileList isUpload={isUpload} onCancelUpload={()=>setIsUpload(false)}/>
                
                {folders.list.length === 0 && files.list.length === 0 && (
                    <Grid item xs={12}>
                        <Typography variant="body1" align="center" className={classes.content}>No file or folder</Typography>
                    </Grid>
                )}
            </Grid>
        </Paper>
        
    </>;
}