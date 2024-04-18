import React, { useState } from "react";
import { CKEditor } from "@ckeditor/ckeditor5-react";
import ClassicEditor from "@ckeditor/ckeditor5-build-classic";

import { Button, Grid, Typography, TextField } from "@material-ui/core";
import UploadAdapterPlugin from "./UploadAdapterPlugin";

import { makeStyles } from "@material-ui/core/styles";
import { useTranslation } from "react-i18next";

const useStyles = makeStyles((theme) => ({
  editor: {
    margin: theme.spacing(1),
    width: "100%",
    height: "300px",
  },
}));

export default function Editor({
  data,
  onChange,
  name,
  label,
  require = false,
}) {
  const [viewHtml, setViewHtml] = useState(false);
  const classes = useStyles();
  const { t } = useTranslation();
  return (
    <>
      <Grid container>
        <Grid item md={6}>
          {label !== "" && (
            <Typography>{label + (require ? " *" : "")}:</Typography>
          )}
        </Grid>
        <Grid item md={6} container justifyContent="flex-end">
          <Button onClick={() => setViewHtml(!viewHtml)}>
            {t("view-source")}
          </Button>
        </Grid>
      </Grid>
      {viewHtml ? (
        <TextField
          id={name}
          name={name}
          multiline
          value={data}
          variant="outlined"
          rows={13}
          InputProps={{ className: classes.editor }}
          onChange={onChange}
        />
      ) : (
        <CKEditor
          editor={ClassicEditor}
          data={data}
          onChange={(event, editor) => {
            onChange({ target: { name, value: editor.getData() } });
          }}
          config={{
            extraPlugins: [UploadAdapterPlugin],
          }}
          onReady={(editor) => {
            editor &&
              editor.editing.view.change((writer) => {
                writer.setStyle(
                  "height",
                  "300px",
                  editor.editing.view.document.getRoot()
                );
              });
          }}
        />
      )}
    </>
  );
}
