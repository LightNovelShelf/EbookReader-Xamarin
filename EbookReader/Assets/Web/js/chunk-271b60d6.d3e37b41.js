(window["webpackJsonp"]=window["webpackJsonp"]||[]).push([["chunk-271b60d6"],{7819:function(e,t,c){},"950a":function(e,t,c){"use strict";c("7819")},ac7f:function(e,t,c){"use strict";c.r(t);var o=c("7a23");const a=e=>(Object(o["K"])("data-v-f84f6c12"),e=e(),Object(o["I"])(),e),b={class:"header",ref:"header"},n=a(()=>Object(o["l"])("div",null,[Object(o["m"])("阅读时长 "),Object(o["l"])("span",{style:{"font-size":"1.5em"}},"114514"),Object(o["m"])(" 小时")],-1)),i=Object(o["m"])(" 打开书籍 "),l=Object(o["m"])(" 打开文件夹 "),r={class:"content"};function j(e,t,c,a,j,O){const d=Object(o["R"])("n-icon"),s=Object(o["R"])("n-button"),u=Object(o["R"])("n-button-group"),p=Object(o["R"])("n-popover"),k=Object(o["R"])("n-space"),f=Object(o["R"])("book-card"),g=Object(o["R"])("n-gi"),m=Object(o["R"])("book-group-card"),v=Object(o["R"])("n-grid");return Object(o["H"])(),Object(o["k"])("div",{class:"wrapper",style:Object(o["A"])(e.property)},[Object(o["l"])("div",b,[Object(o["n"])(k,{justify:"space-between",align:"center"},{default:Object(o["cb"])(()=>[n,Object(o["l"])("div",null,[Object(o["n"])(k,null,{default:Object(o["cb"])(()=>[Object(o["n"])(s,{text:""},{default:Object(o["cb"])(()=>[Object(o["n"])(d,{size:"24",class:"icon"},{default:Object(o["cb"])(()=>[Object(o["m"])(Object(o["U"])(e.icon.mdiMagnify),1)]),_:1})]),_:1}),Object(o["n"])(p,{placement:"bottom-end",trigger:"click",style:{padding:"0"}},{trigger:Object(o["cb"])(()=>[Object(o["n"])(s,{text:""},{default:Object(o["cb"])(()=>[Object(o["n"])(d,{size:"24",class:"icon"},{default:Object(o["cb"])(()=>[Object(o["m"])(Object(o["U"])(e.icon.mdiDotsVertical),1)]),_:1})]),_:1})]),default:Object(o["cb"])(()=>[Object(o["n"])(u,{vertical:"",size:"large"},{default:Object(o["cb"])(()=>[Object(o["n"])(s,{onClick:e.chooseBook},{default:Object(o["cb"])(()=>[i]),_:1},8,["onClick"]),Object(o["n"])(s,{onClick:e.chooseDir},{default:Object(o["cb"])(()=>[l]),_:1},8,["onClick"])]),_:1})]),_:1})]),_:1})])]),_:1})],512),Object(o["l"])("div",r,[Object(o["n"])(v,{"x-gap":"12","y-gap":"8",cols:3},{default:Object(o["cb"])(()=>{var t;return[e.gid?(Object(o["H"])(!0),Object(o["k"])(o["b"],{key:0},Object(o["P"])(null===(t=e.bookList)||void 0===t?void 0:t.data,e=>(Object(o["H"])(),Object(o["i"])(g,{key:e.id},{default:Object(o["cb"])(()=>[Object(o["n"])(f,{book:e},null,8,["book"])]),_:2},1024))),128)):(Object(o["H"])(!0),Object(o["k"])(o["b"],{key:1},Object(o["P"])(e.bookList,e=>(Object(o["H"])(),Object(o["i"])(g,{key:e.id},{default:Object(o["cb"])(()=>["BookGroupCard"===e.type?(Object(o["H"])(),Object(o["i"])(m,{key:0,"book-list":e.data,id:e.id,"group-name":e.groupName},null,8,["book-list","id","group-name"])):(Object(o["H"])(),Object(o["i"])(f,{key:1,book:e.data},null,8,["book"]))]),_:2},1024))),128))]}),_:1})])],4)}var O=c("c872"),d=c("9381"),s=c("3519"),u=c("9a21"),p=c("646f"),k=c("2649"),f=c("5927"),g=c("a183");function m(){const e=Object(o["t"])(f["a"],null);return Object(o["g"])(()=>{if(null===e)return g["a"];const{mergedThemeRef:{value:t},mergedThemeOverridesRef:{value:c}}=e,o=(null===t||void 0===t?void 0:t.common)||g["a"];return(null===c||void 0===c?void 0:c.common)?Object.assign({},o,c.common):o})}var v=c("eaca"),h=c("cfe7"),y=c("7f6a"),B=c("6c02"),w=c("6022");const _=c("df7c");var R=Object(o["o"])({name:"BookShelf",props:{gid:String},components:{NSpace:O["a"],NButtonGroup:d["b"],NButton:s["a"],NPopover:u["a"],NGrid:p["a"],NGi:k["a"],BookGroupCard:h["b"],BookCard:h["a"]},setup(e){const t=m(),c=Object(B["d"])(),a=Object(y["a"])(),b=a.getBookList;a.init();const n=Object(o["t"])("chooseFile");return{bookList:Object(o["g"])(()=>b(e.gid)),icon:v["a"],property:Object(o["g"])(()=>({"--border-size":"1px","--border-color":t.value.borderColor,"--border-radius":t.value.borderRadius,"--opacity-2":t.value.opacity2})),async chooseBook(){console.log("chooseBook");let e=await n.chooseFile("epub");console.log(e),await c.push({name:"Read",params:{path:e}});let t=await Object(w["b"])(e);a.getBookByPath(e)||a.addBook(t.id,{title:t.title,cover:t.cover,path:e})},async chooseDir(){console.log("chooseDir");let e=await n.chooseDir(),t=async e=>{const c=[];let o=await Object(w["e"])(e),b=await Object(w["a"])(e);o.filter(e=>e.endsWith("epub")).map(e=>{a.getBookByPath(e)||c.push(e)}),0!==c.length&&a.addBookGroup(_.basename(e),c.map(e=>({path:e}))),b.forEach(e=>{t(e)})};await t(e)}}}}),C=(c("950a"),c("6b0d")),H=c.n(C);const N=H()(R,[["render",j],["__scopeId","data-v-f84f6c12"]]);t["default"]=N}}]);