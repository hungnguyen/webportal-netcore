import React from "react";
import {
  Button,
  Dialog,
  DialogActions,
  DialogContent,
  DialogContentText,
  DialogTitle,
} from "@material-ui/core";

export default function ConfirmDialog(props) {
  return (
    <div>
      <Dialog
        open={props.open}
        onClose={props.handleClose}
        aria-labelledby="alert-dialog-title"
        aria-describedby="alert-dialog-description"
      >
        <DialogTitle id="alert-dialog-title">{props.title}</DialogTitle>
        <DialogContent>
          <DialogContentText id="alert-dialog-description">
            {props.message}
          </DialogContentText>
        </DialogContent>
        <DialogActions>
          <Button onClick={props.handleClose} color="primary">
            {props.cancelButton ?? "Cancel"}
          </Button>
          <Button onClick={props.handleConfirm} color="primary" autoFocus variant="contained">
            {props.confirmButton ?? "Ok"}
          </Button>
        </DialogActions>
      </Dialog>
    </div>
  );
}
