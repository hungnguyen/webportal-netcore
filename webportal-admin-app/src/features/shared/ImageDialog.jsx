import React from "react";
import {
  Button,
  Dialog,
  DialogActions,
  DialogContent,
  DialogContentText,
  DialogTitle,
} from "@material-ui/core";

export default function ImageDialog(props) {
  return (
    <div>
      <Dialog
        maxWidth="lg"
        open={props.open}
        onClose={props.handleClose}
        aria-labelledby="alert-dialog-title"
        aria-describedby="alert-dialog-description"
      >
        <DialogTitle id="alert-dialog-title">{props.title}</DialogTitle>
        <DialogContent>
          <DialogContentText id="alert-dialog-description">
            <img src={`${props.imageSrc}`} alt={props.title} title={props.title} style={{maxHeight: "calc(100vh - 200px)", maxWidth: "100%"}} />
          </DialogContentText>
        </DialogContent>
        <DialogActions>
          <Button onClick={props.handleClose} color="primary">
            {props.confirmButton ?? "Close"}
          </Button>
        </DialogActions>
      </Dialog>
    </div>
  );
}
