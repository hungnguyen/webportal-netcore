import React, { useState, useEffect } from "react";
import { useSelector, useDispatch } from "react-redux";
import { productFileSelector } from "../../redux/productFile/productFileSlice";
import {
  getAllAsync,
  removeAsync,
} from "../../redux/productFile/productFileAsyncThunk";
import useStyles from "../shared/styles";
import { NavLink, useParams, useHistory } from "react-router-dom";
import ConfirmDialog from "../shared/ConfirmDialog";
import TableView from "../shared/TableView";
import { Paper, IconButton, Button, Tooltip } from "@material-ui/core";
import { Edit, Delete } from "@material-ui/icons";
import { applicationSelector } from "../../redux/application/applicationSlice";
import { useTranslation } from "react-i18next";

export default function List() {
  const { productid, type } = useParams();
  const history = useHistory();
  const dispatch = useDispatch();
  const productFile = useSelector(productFileSelector);
  const application = useSelector(applicationSelector);
  const classes = useStyles();
  const [openConfirmDelete, setOpenConfirmDelete] = useState(false);
  const [itemDelete, setItemDelete] = useState({});
  const [productFileByPID, setProductFileByPID] = useState([]);
  const { t } = useTranslation();

  useEffect(() => {
    if (productFile.list.length === 0) {
      dispatch(getAllAsync());
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  useEffect(() => {
    setProductFileByPID(
      productFile.list.filter((i) => i.productid === parseInt(productid))
    );
  }, [productFile.list, productid]);

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
      field: "image",
      headerName: t("image"),
      flex: 1,
      renderCell: (params) => (
        <img
          src={`${application.imageBaseAddress}/${params.row.filename}`}
          alt=""
          height="100%"
        />
      ),
    },
    { field: "name", headerName: t("name"), flex: 1 },
    { field: "filename", headerName: t("file-name"), flex: 1 },
    { field: "ordernumber", headerName: t("order-number"), flex: 1 },
    {
      field: "id",
      headerName: t("action"),
      flex: 0.5,
      renderCell: (params) => (
        <strong>
          <NavLink to={`/product-file/${type}/${productid}/edit/${params.id}`}>
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

  const handleBack = () => {
    history.push(`/product/${type}`);
  };
  return (
    <>
      <Button
        variant="outlined"
        onClick={handleBack}
        className={classes.saveButton}
      >
        {t("back-to-product")}
      </Button>
      <Button variant="outlined" color="primary">
        <NavLink
          to={`/product-file/${type}/${productid}/edit`}
          className={classes.link}
        >
          {t("add-new")}
        </NavLink>
      </Button>
      <Paper className={classes.tablePaper}>
        <TableView
          title={t("list")}
          rows={productFileByPID}
          loading={productFile.loading}
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
