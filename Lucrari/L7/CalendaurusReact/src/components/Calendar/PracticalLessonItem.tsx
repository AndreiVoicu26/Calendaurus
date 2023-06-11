import {
  Box,
  Paper,
  Button,
  Dialog,
  DialogActions,
  DialogContent,
  DialogContentText,
  DialogTitle,
} from "@mui/material";
import { useState } from "react";
import { PracticalLessonEvent, PracticalLessonEventEnrollment } from "../../actions/models";
import { enrollStudent, unenrollStudent } from "../../actions/api";
import { useMsal } from "@azure/msal-react";

type PracticalLessonItemProps = {
  disciplineName?: string;
  practicalLessonDescription?: string;
  practicalLessonEvent?: PracticalLessonEvent;
  isEnrolled?: boolean;
  updateIsEnrolled?: (enrolled?: boolean) => void;
};

export const PracticalLessonItem = ({
  disciplineName,
  practicalLessonDescription,
  practicalLessonEvent,
  isEnrolled,
  updateIsEnrolled,
}: PracticalLessonItemProps) => {
  const [isModalOpen, setIsModalOpen] = useState(false);
  const { instance } = useMsal();

  const handleClose = (enrolled?: boolean) => {
    setIsModalOpen(false);
    enrolled !== undefined && updateIsEnrolled?.();
  };

  const handleSave = () => {
    const practicalLessonEventId = practicalLessonEvent?.id;
    if (practicalLessonEventId) {
      const practicalLessonEnrollment: PracticalLessonEventEnrollment = {
        practicalLessonEventId: practicalLessonEventId,
      };
      if (isEnrolled) {
        unenrollStudent(
          instance,
          () => handleClose(false),
          practicalLessonEventId
        );
      } else {
        enrollStudent(
          instance,
          () => handleClose(true),
          practicalLessonEnrollment
        );
      }
    }
  };

  return (
    <>
      <Box onClick={() => setIsModalOpen(true)}>
        <Paper
          sx={{
            backgroundColor: isEnrolled ? "#83de92" : "white",
            padding: "0rem 0.5rem",
            "&:hover": {
              opacity: 0.8,
            },
            cursor: "pointer",
          }}
          elevation={3}
        >
          {disciplineName}: {practicalLessonDescription}
        </Paper>
      </Box>
      <Dialog
        open={isModalOpen}
        onClose={() => handleClose()}
        aria-labelledby="alert-dialog-title"
        aria-describedby="alert-dialog-description"
      >
        <DialogTitle id="alert-dialog-title">
          {disciplineName}: {practicalLessonDescription}
        </DialogTitle>
        <DialogContent>
          <DialogContentText id="alert-dialog-description">
            Are you sure you want to {isEnrolled ? "unenroll" : "enroll"} to
            this class?
          </DialogContentText>
        </DialogContent>
        <DialogActions>
          <Button onClick={handleSave}>
            {isEnrolled ? "Unenroll" : "Enroll"}
          </Button>
          <Button onClick={() => handleClose()}>Cancel</Button>
        </DialogActions>
      </Dialog>
    </>
  );
};
