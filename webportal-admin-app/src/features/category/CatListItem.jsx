import {
  Collapse,
  IconButton,
  List,
  ListItem,
  ListItemIcon,
  ListItemSecondaryAction,
  ListItemText,
  Tooltip,
} from "@material-ui/core";
import { removeAsync } from "../../redux/category/categoryAsyncThunk";
import { Delete, Edit, ExpandLess, ExpandMore } from "@material-ui/icons";
import React, { useEffect, useState } from "react";
import { NavLink, useHistory } from "react-router-dom";
import useStyles from "../shared/styles";
import CatListView from "./CatListView";
import ConfirmDialog from "../shared/ConfirmDialog";
import { useDispatch } from "react-redux";
import { useTranslation } from "react-i18next";
import { equals } from "../shared/utils";

const CatListItem = ({ item, all, className }) => {
  const dispatch = useDispatch();
  const [open, setOpen] = useState(true);
  const classes = useStyles();
  const [list, setList] = useState([]);
  const [openConfirmDelete, setOpenConfirmDelete] = useState(false);
  const [itemDelete, setItemDelete] = useState({});
  const { t } = useTranslation();
  const history = useHistory();

  const handleDelete = (item) => {
    setOpenConfirmDelete(true);
    setItemDelete(item);
  };

  const handleConfirm = () => {
    dispatch(removeAsync(itemDelete.id));
    setOpenConfirmDelete(false);
  };

  const handleCose = () => {
    setOpenConfirmDelete(false);
  };

  const handleClick = (id) => {
    if (list.length === 0) history.push(`/category/edit/${item.id}`);
    else setOpen(!open);
  };

  useEffect(() => {
    setList(all.filter((i) => equals(i.parentid, item.id)));
  }, [all, item.id]);

  return (
    <>
      <ListItem
        button
        onClick={() => handleClick(item.id)}
        className={className}
      >
        <ListItemIcon>
          {list.length > 0 && (open ? <ExpandLess /> : <ExpandMore />)}
        </ListItemIcon>
        <ListItemText primary={item.name} />
        <ListItemSecondaryAction>
          <NavLink to={`/category/edit/${item.id}`}>
            <Tooltip title={t("edit")}>
              <IconButton color="primary" size="small">
                <Edit />
              </IconButton>
            </Tooltip>
          </NavLink>
          <Tooltip title={t("delete")}>
            <IconButton
              color="secondary"
              size="small"
              onClick={() => handleDelete(item)}
            >
              <Delete />
            </IconButton>
          </Tooltip>
        </ListItemSecondaryAction>
      </ListItem>
      {list.length > 0 && (
        <Collapse in={open} timeout="auto" unmountOnExit>
          <List component="div" disablePadding className={classes.nested}>
            <CatListView all={all} parentid={item.id} />
          </List>
        </Collapse>
      )}
      <ConfirmDialog
        title={t("confirm-delete")}
        message={t("are-you-sure-want-to-delete", {
          itemName: itemDelete.name,
        })}
        open={openConfirmDelete}
        handleClose={handleCose}
        handleConfirm={handleConfirm}
      />
    </>
  );
};

export default CatListItem;
