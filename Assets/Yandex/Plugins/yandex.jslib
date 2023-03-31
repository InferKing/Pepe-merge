mergeInto(LibraryManager.library, {

  ShowAdvVideo : function(value){
    ysdk.adv.showRewardedVideo({
      callbacks: {
        onOpen: () => {
          console.log('Video ad open.');
          myGameInstance.SendMessage("FocusSoundController", "OffMusic");
        },
        onRewarded: () => {
          console.log('Rewarded!');
        },
        onClose: () => {
          myGameInstance.SendMessage("FocusSoundController", "OnMusic");
          console.log('Video ad closed.');
        }, 
        onError: (e) => {
          console.log('Error while open video ad:', e);
        }
      }
    })
  },
    ShowAdvFull : function(){
    ysdk.adv.showFullscreenAdv({
    callbacks: {
        onClose: function(wasShown) {
          // some action after close
        },
        onError: function(error) {
          // some action on error
        }
    }
    })
  },

  SaveExtern: function(date) {
    var dateString = UTF8ToString(date);
    var myobj = JSON.parse(dateString);
    player.setData(myobj);
  },

  LoadExtern: function() {
    player.getData().then(_date => {
      const myJSON = JSON.stringify(_date);
      console.log(myJSON);
      myGameInstance.SendMessage('Player', 'SetData', myJSON);
    });
  },

  SetCurScore: function(value) {
    ysdk.getLeaderboards()
    .then(lb => {
      lb.setLeaderboardScore('Score', value);
    });
  },

  GetLang : function() {
    var lang = ysdk.environment.i18n.lang;
    var bufferSize = lengthBytesUTF8(lang) + 1;
    var buffer = _malloc(bufferSize);
    stringToUTF8(lang, buffer, bufferSize);
    return buffer;
  },

});