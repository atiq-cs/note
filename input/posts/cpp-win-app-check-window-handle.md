Title: Multi-threaded Windows C++ Application
Lead: Check Window Handle before Updating Child Items
Published: 07/08/2011
Tags:
  - windows programming
  - C++
  - MFC
---
In an MFC Application, you might use a thread to update some control of a dialog box or Window. Now, while this application is running, user may press any of the buttons for example, OK, Cancel or simply close the Windows or Dialog without performing any of the options while your thread is still performing some background operation. After the operation is complete it tries to update Windows/Dialog. In the meantime, because user has already closed the Window an attempt to update the interface will cause application to crash due to invalid handle.

Windows will report the crash with a dialog box that your application has stopped working.

To ensure this does not happen for a Windows App you need to check whether that dialog box window really exists before updating it. To do that we use `IsWindow` function and pass the Windows Handle Pointer. The function we use here to update some text control in the Window/Dialog is `SetDlgItemText` Here's an example,

    int res = PerformSomeTask(param);
    
    if (res == 0) {
        if (::IsWindow(ts->_this->m_hWnd) == TRUE) {  // ts is thread structure
            SetDlgItemText(IDC_CHECKNOTIFY, _T("Service is not available"));    // IDC_CHECKNOTIFY is a static text control
        }
    }
    else {
        if (::IsWindow(ts->_this->m_hWnd) == TRUE)
            SetDlgItemText(IDC_CHECKNOTIFY, _T("Service is available"));
    }
