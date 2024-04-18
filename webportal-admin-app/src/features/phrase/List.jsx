import React, { useState, useEffect } from "react";
import { useSelector, useDispatch } from "react-redux";
import { phraseSelector } from "../../redux/phrase/phraseSlice";
import {
  getPagingAsync,
  removeAsync,
} from "../../redux/phrase/phraseAsyncThunk";
import useStyles from "../shared/styles";
import { NavLink } from "react-router-dom";
import ConfirmDialog from "../shared/ConfirmDialog";
import TableView from "../shared/TableView";
import { Paper, IconButton, Button, Tooltip } from "@material-ui/core";
import { Edit, Delete } from "@material-ui/icons";
import { applicationSelector } from "../../redux/application/applicationSlice";
import { useTranslation } from "react-i18next";

export default function List() {
  const dispatch = useDispatch();
  const phrase = useSelector(phraseSelector);
  const classes = useStyles();
  const [openConfirmDelete, setOpenConfirmDelete] = useState(false);
  const [itemDelete, setItemDelete] = useState({});
  const application = useSelector(applicationSelector);
  const { t } = useTranslation();

  const loadData = () => {
    dispatch(
      getPagingAsync({
        pagesize: 0,
        websiteid: application.website.id,
        languageid: application.languageid,
      })
    );
  };

  useEffect(() => {
    if (phrase.list.length === 0) {
      loadData();
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
      field: "key",
      headerName: t("key"),
      flex: 0.2,
      renderCell: (params) => (
        <NavLink to={`/phrase/edit/${params.id}`} className={classes.link}>
          {params.row.key}
        </NavLink>
      ),
    },
    { field: "value", headerName: t("value"), flex: 1 },
    {
      field: "id",
      headerName: t("action"),
      flex: 0.1,
      renderCell: (params) => (
        <strong>
          <NavLink to={`/phrase/edit/${params.id}`}>
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
        <NavLink to="/phrase/edit" className={classes.link}>
          {t("add-new")}
        </NavLink>
      </Button>
      <Paper className={classes.tablePaper}>
        <TableView
          title={t("list")}
          rows={phrase.list}
          loading={phrase.loading}
          columns={columns}
          searchColumn={["key", "value"]}
          onRefresh={loadData}
        />
      </Paper>
      <ConfirmDialog
        title={t("confirm-delete")}
        message={t("are-you-sure-want-to-delete", {
          itemName: itemDelete.key,
        })}
        open={openConfirmDelete}
        handleClose={handleCose}
        handleConfirm={handleConfirm}
      />
    </>
  );
}
