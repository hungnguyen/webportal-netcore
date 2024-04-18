import React, { useState } from "react";
import { useSelector } from "react-redux";
import { logSelector } from "../../redux/loginfo/logSlice";

import useStyles from "../shared/styles";
import DetailDialog from "../shared/DetailDialog";
import TableView from "../shared/TableView";
import { Paper, IconButton, Tooltip } from "@material-ui/core";
import { Visibility } from "@material-ui/icons";
import { useTranslation } from "react-i18next";

export default function List() {
  const log = useSelector(logSelector);
  const classes = useStyles();
  const [openDetail, setOpenDetail] = useState(false);
  const [itemView, setItemView] = useState({});
  const { t } = useTranslation();

  const handleView = (row) => {
    setItemView(row);
    setOpenDetail(true);
  };

  const handleCose = () => {
    setOpenDetail(false);
  };

  const columns = [
    { field: "statusCode", headerName: t("statusCode"), flex: 1 },
    { field: "message", headerName: t("message"), flex: 1 },
    {
      field: "dummy",
      headerName: t("action"),
      flex: 0.5,
      renderCell: (params) => (
        <strong>
          <Tooltip title={t("view")}>
            <IconButton
              color="primary"
              size="small"
              onClick={() => handleView(params.row)}
            >
              <Visibility />
            </IconButton>
          </Tooltip>
        </strong>
      ),
    },
  ];

  return (
    <>
      <Paper className={classes.tablePaper}>
        <TableView
          title={t("list")}
          rows={log.list}
          columns={columns}
          searchColumn={["statusCode", "message"]}
        />
      </Paper>
      <DetailDialog
        title={t("detail")}
        message={itemView.detail ?? itemView.message}
        open={openDetail}
        handleClose={handleCose}
      />
    </>
  );
}
