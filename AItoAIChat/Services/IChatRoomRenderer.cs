using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AItoAIChat.Services;
public interface IChatRoomRenderer
{
    ValueTask RenderAsync(ChatRoom chatRoom);
}
