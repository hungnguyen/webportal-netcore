import React, { useState, useEffect } from "react";
import { useSelector, useDispatch } from "react-redux";
import { approleSelector } from "../../redux/approle/approleSlice";
import {
  getAllAsync,
  removeAsync,
} from "../../redux/approle/approleAsyncThunk";
import useStyles from "../shared/styles";
import { NavLink } from "react-router-dom";
import ConfirmDialog from "../shared/ConfirmDialog";
import TableView from "../shared/TableView";
import { Paper, IconButton, Button, Tooltip } from "@material-ui/core";
import { Edit, Delete } from "@material-ui/icons";
import { useTranslation } from "react-i18next";

export default function List() {
  const dispatch = useDispatch();
  const approle = useSelector(approleSelector);
  const classes = useStyles();
  const [openConfirmDelete, setOpenConfirmDelete] = useState(false);
  const [itemDelete, setItemDelete] = useState({});
  const { t } = useTranslation();

  useEffect(() => {
    if (approle.list.length === 0) {
      dispatch(getAllAsync());
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

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

  const columns = [
    {
      field: "name",
      headerName: t("name"),
      flex: 1,
      renderCell: (params) => (
        <NavLink to={`/approle/edit/${params.id}`} className={classes.link}>
          {params.row.name}
        </NavLink>
      ),
    },
    { field: "description", headerName: t("description"), flex: 1 },

    {
      field: "id",
      headerName: t("action"),
      flex: 0.5,
      renderCell: (params) => (
        <strong>
          <NavLink to={`/approle/edit/${params.id}`}>
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
              onClick={() => handleDelete(params.row)}
            >
              <Delete />
            </IconButton>
          </Tooltip>
        </strong>
      ),
    },
  ];

  return (
    <>
      <Button variant="outlined" color="primary">
        <NavLink to="/approle/edit" className={classes.link}>
          {t("add-new")}
        </NavLink>
      </Button>
      <Paper className={classes.tablePaper}>
        <TableView
          title={t("list")}
          rows={approle.list}
          loading={approle.loading}
          columns={columns}
          searchColumn={["name"]}
          onRefresh={() => dispatch(getAllAsync())}
        />
      </Paper>
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
}
