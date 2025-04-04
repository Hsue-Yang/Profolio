import React from "react";
import { post } from "../services/api";
import { apiUrl } from "../services/apiUrl";
// ErrorBoundary以內的child element有發生錯誤都會被捕捉
// 以下四種情況不會被捕捉
// Event handlers、非同步的程式碼 (例如 setTimeout 或 requestAnimationFrame callback)
// Server side rendering、在錯誤邊界裡丟出的錯誤（而不是在它底下的 children）
class ErrorBoundary extends React.Component {
    constructor(props) {
        super(props);
        this.state = { hasError: false };
    }
    //當子組件發生錯誤時觸發，用來更新UI
    static getDerivedStateFromError(error) {
        // 更新 state 以至於下一個 render 會顯示 fallback UI
        return { hasError: true };
    }
    //捕捉錯誤和錯誤資訊
    componentDidCatch(error, errorInfo) {
        post(apiUrl.errorLog, {
            error: error.toString(),
            errorInfo: errorInfo.componentStack,
        }).catch((e) => {
            console.error("Error logging failed", e);
        });
        console.log('componentDidCatch', { error, errorInfo });
    }
    //提供替代的UI
    render() {
        if (this.state.hasError) {
            return (
                <div style={{ padding: "20px", textAlign: "center" }}>
                    <h1 style={{ color: "red" }}>Oops! Something went wrong.</h1>
                    <p>We're sorry for the inconvenience. Please try again later.</p>
                    <button onClick={() => window.location.reload()} style={{ padding: "10px 20px", fontSize: "16px" }}>
                        Reload Page
                    </button>
                </div>
            );
        }

        return this.props.children;
    }
}

export default ErrorBoundary;