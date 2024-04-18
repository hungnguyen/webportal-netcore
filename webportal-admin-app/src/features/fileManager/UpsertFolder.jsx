import React,{useEffect, useState} from "react";
import {useDispatch} from "react-redux";
import {createAsync, updateAsync} from "../../redux/folder/folderAsyncThunk";
import {Dialog, DialogTitle, DialogContent, TextField, DialogActions, Button} from "@material-ui/core";

export default function UpsertFolder({open, onClose, item, isCreate}){
    const dispatch = useDispatch();
    const [currentItem, setCurrentItem] = useState({...item});

    useEffect(()=>{
        setCurrentItem({...item});
    },[item]);

    const handleSave = () =>{
        onClose();
        if(isCreate){
            dispatch(createAsync(currentItem));
        } else {
            dispatch(updateAsync(currentItem));
        }
    }
    const handleChange = (e) =>{
        const {name, value} = e.target;
        setCurrentItem({...currentItem, [name]: value});
    }
    return(
        <>
            <Dialog open={open} onClose={onClose} aria-labelledby="form-dialog-title">
                <DialogTitle id="form-dialog-title">Add/Edit Folder</DialogTitle>
                <DialogContent>
                
                <TextField
                    autoFocus
                    margin="dense"
                    name="name"
                    label="Name"
                    type="text"
                    value={currentItem.name}
                    onChange={handleChange}
                    fullWidth
                />
                </DialogContent>
                <DialogActions>
                <Button onClick={onClose} color="primary">
                    Cancel
                </Button>
                <Button onClick={handleSave} color="primary">
                    Save
                </Button>
                </DialogActions>
            </Dialog>
        </>
    );
}