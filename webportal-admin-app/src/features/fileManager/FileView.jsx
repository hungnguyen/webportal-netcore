import React, { useState } from "react";
import { useSelector, useDispatch } from 'react-redux';
import {Box, Typography, Menu, MenuItem} from "@material-ui/core";
import {Description} from "@material-ui/icons";
import {cutString} from "../shared/stringUtils";
import { applicationSelector } from "../../redux/application/applicationSlice";
import { getFileExtension } from "../shared/utils";
import ImageDialog from "../shared/ImageDialog";
import { select, open } from "../../redux/file/fileSlice";

export default function FileView({data, className, isSelected, onUpdate, onDelete}) {
    const application = useSelector(applicationSelector);
    const dispatch = useDispatch();
    const [openDetail, setOpenDetail] = useState(false);
    const initContextMenu = {
        mouseX: null,
        mouseY: null,
      };
    const [contextMenu, setContextMenu] = useState(initContextMenu);

    const handleContextMenu = (event) => {
        event.preventDefault();
        dispatch(select(data));
        setContextMenu({
        mouseX: event.clientX - 2,
        mouseY: event.clientY - 4,
        });
    };

    const handleCloseContextMenu = () => {
        setContextMenu(initContextMenu);
    };

    const isImage = (fileName) => {
        return "jpg,png".indexOf(getFileExtension(fileName)) > -1;
    }
    const handleView = (row) => {
        dispatch(open(data));
        setOpenDetail(true);
    };

    const handleCose = () => {
        setOpenDetail(false);
    };
    const handleUpdate=()=>{
        onUpdate();
        setContextMenu(initContextMenu);
    }
    const handleDelete=()=>{
        onDelete();
        setContextMenu(initContextMenu);
    }
    const FileDetail = () =>{
        return (
            <>
                {isImage(data.name)?(
                    <img src={`${application.imageBaseAddress}/${data.path}`} alt={data.name} title={data.name} height={50} width={50} />
                ):(
                    <Description style={{ fontSize: 40 }} />
                )}
                <Typography variant="caption" display="block" gutterBottom title={data.name}>
                    {cutString(data.name, 15)}
                </Typography>
            </>
        );
    }
    return (
        <>
            <Box className={`${className} ${isSelected?"selected":""}`} onClick={handleView} onContextMenu={handleContextMenu}>
                <FileDetail />
            </Box>

            <ImageDialog
                title={data.name}
                imageSrc={`${application.imageBaseAddress}/${data.path}`}
                open={openDetail}
                handleClose={handleCose}
            />

            <Menu
                keepMounted
                open={contextMenu.mouseY !== null}
                onClose={handleCloseContextMenu}
                anchorReference="anchorPosition"
                anchorPosition={
                    contextMenu.mouseY !== null && contextMenu.mouseX !== null
                    ? { top: contextMenu.mouseY, left: contextMenu.mouseX }
                    : undefined
                }
            >
                <MenuItem onClick={handleUpdate}>Rename</MenuItem>
                <MenuItem onClick={handleDelete}>Delete</MenuItem>
            </Menu>

        </>
        
    );
};