import React, {useState, useEffect} from "react";
import { Dialog, DialogTitle, DialogContent, TextField, DialogActions, Button } from "@material-ui/core";
import {useDispatch} from "react-redux";
import { updateAsync, createAsync } from "../../redux/file/fileAsyncThunk";
import useStyles from "../shared/styles";

export default function UpsertFile({open, item, onClose, isCreate}){
    const dispatch = useDispatch();
    const classes = useStyles();
    const [currentItem, setCurrentItem] = useState({...item});
    const [imageFile, setImageFile] = useState(null);

    useEffect(()=>{
        setCurrentItem({...item});
    },[item]);

    const handleSave = () =>{
        onClose();
        if(isCreate){
            dispatch(createAsync(getFormData()));
        } else {
            dispatch(updateAsync(currentItem));
        }
    }
    const handleChange = (e) =>{
        const {name, value} = e.target;
        setCurrentItem({...currentItem, [name]: value});
    }
    
    const handleImageChange = (e) => {
        // Assuming only image
        var file = e.target.files[0];
        
        setImageFile(file);
    };
    
    const getFormData = () => {
        if (imageFile !== null) {
            let formData = new FormData();
            formData.append("file", imageFile, imageFile.name);
            formData.append("name", currentItem.name);
            formData.append("path", currentItem.parent);
            return formData;
        }
        return null;
    };

    return (
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
                {isCreate && (
                    <>
                        <input
                            accept="image/*"
                            className={classes.hidden}
                            id="contained-button-file"
                            multiple
                            type="file"
                            onChange={handleImageChange}
                        />
                        <label htmlFor="contained-button-file">
                            <Button variant="outlined" color="primary" component="span">
                            Browse
                            </Button>
                        </label>
                        <span>{imageFile?.name}</span>
                    </>
                )}

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