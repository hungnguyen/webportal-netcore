import React, { useState } from "react";
import {Box, Menu, MenuItem, Typography} from "@material-ui/core";
import {Folder} from "@material-ui/icons";
import {cutString} from "../shared/stringUtils";
import {select, open} from "../../redux/folder/folderSlice"
import { useDispatch } from "react-redux";

export default function FolderView({data, className, isSelected, onUpdate, onDelete}) {
    const dispatch = useDispatch();
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
    const handleDbClick=()=>{
        dispatch(open(data));
    }
    const handleUpdate = () => {
        onUpdate();
        setContextMenu(initContextMenu);
    }
    const handleDelete = () => {
        onDelete();
        setContextMenu(initContextMenu);
    }
    
    const FolderDetail = () =>{
        return (
            <>
                <Folder />
                <Typography variant="caption" display="block" gutterBottom title={data.name}>
                    {cutString(data.name, 15)}
                </Typography>
            </>
        );
    }
    return (
        <>
            <Box className={`${className} ${isSelected?"selected":""} folder`} onDoubleClick={handleDbClick} onContextMenu={handleContextMenu}>
                <FolderDetail />
            </Box>
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